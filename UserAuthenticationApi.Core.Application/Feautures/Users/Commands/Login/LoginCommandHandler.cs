using AutoMapper;
using MediatR;
using UserAuthenticationApi.Core.Application.Common;
using UserAuthenticationApi.Core.Application.Dtos;
using UserAuthenticationApi.Core.Application.Helpers;
using UserAuthenticationApi.Core.Application.Interfaces.IRepositories;
using UserAuthenticationApi.Core.Application.Interfaces.IServices;

namespace UserAuthenticationApi.Core.Application.Feautures.Users.Commands.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, UsersDto>
    {
            
        private readonly IUsersRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IJwtGeneratorService _jwtGeneratorService;
        public LoginCommandHandler(
            IUsersRepository userRepository,
            IMapper mapper,
            IJwtGeneratorService jwtGeneratorService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _jwtGeneratorService = jwtGeneratorService;
        }
        public async Task<UsersDto> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
           var hashPassword = PasswordEncryptator.HashUserPassword384(request.Password);

           var emailVerifier = await _userRepository.EmailExistanceAsync(request.Email);
           var passwordVerifier = await _userRepository.VerifyPasswordAsync(hashPassword);
            if (!emailVerifier || !passwordVerifier) throw new ApiException("Email o contraseña incorrectos trate de nuevo",400);

            var user = await _userRepository.GetUserByEmail(request.Email);
            if(user == null) throw new ApiException("El usuario no pudo ser encontrado", 404);

            user.LastLogin = DateTime.Now;
            user.ModifiedDate = DateTime.Now;

            await _userRepository.UpdateAsync(user, user.Id);
            var userDto = new UsersDto
            {
                CreatedDate =  user.CreatedDate,
                Email = request.Email,
                ModifiedDate = user.ModifiedDate,
                Id = user.Id,
                IsActive = user.IsActive,
                LastLogin = user.LastLogin,
                Name = user.Name,
                Password = user.Password,
                Token = user.Token,
                Phones = user.Phones.Select(x => new PhonesDto
                {
                    CityCode = x.CityCode,
                    CountryCode = x.CountryCode,
                    Id = x.Id,
                    UserId = user.Id,
                    Number = x.Number,
                }).ToList(),
            };

            return userDto;
        }
    }
}
