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
            if (!emailVerifier || !passwordVerifier) throw new ApiExeption("Email o contraseña incorrectos trate de nuevo",400);

            var user = await _userRepository.GetUserByEmail(request.Email);
            var token = await _jwtGeneratorService.GenerateJwt(user);

            user.Token = token;
            user.LastLogin = DateTime.Now;
            user.ModifiedDate = DateTime.Now;

            await _userRepository.UpdateAsync(user, user.Id);
            var userDto = _mapper.Map<UsersDto>(user);

            return userDto;
        }
    }
}
