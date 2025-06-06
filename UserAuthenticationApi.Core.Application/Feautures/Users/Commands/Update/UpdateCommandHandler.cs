using AutoMapper;
using MediatR;
using UserAuthenticationApi.Core.Application.Common;
using UserAuthenticationApi.Core.Application.Helpers;
using UserAuthenticationApi.Core.Application.Interfaces.IRepositories;
using UserAuthenticationApi.Core.Application.Interfaces.IServices;
using UserAuthenticationApi.Core.Domain.Entities;

namespace UserAuthenticationApi.Core.Application.Feautures.Users.Commands.Update
{
    public class UpdateCommandHandler : IRequestHandler<UpdateCommand, Unit>
    {
        private readonly IUsersRepository _userRepository;
        private readonly IJwtGeneratorService _jwtGeneratorService;
        private readonly IMapper _mapper;

        public UpdateCommandHandler(
            IUsersRepository userRepository,
            IJwtGeneratorService jwtGeneratorService,
            IMapper mapper)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _jwtGeneratorService = jwtGeneratorService;
        }

        public async Task<Unit> Handle(UpdateCommand commnad, CancellationToken cancellationToken)
        {
            var user  = await _userRepository.GetByIdAsync(commnad.Id);
            if (user == null) throw new ApiException($"No se pudo encontrar una entidad bajo este id {commnad.Id}",404);

            user.Email = commnad.Email ?? user.Email;
            user.Name = commnad.Name ?? user.Name;
            user.Password = commnad.Password ?? user.Password;
            user.IsActive = commnad.IsActive;

            if (commnad.Phones != null && commnad.Phones.Any())
            {
                user.Phones = commnad.Phones
                    .Select(p => new Phone
                    {
                        Number = p.Number,
                        CityCode = p.CityCode,
                        CountryCode = p.CountryCode
                    })
                    .ToList();
            }

            user.Password = PasswordEncryptator.HashUserPassword384(commnad.Password);

            user.ModifiedDate = DateTime.UtcNow;
            await _userRepository.UpdateAsync(user, user.Id);
            return Unit.Value;
        }
    }
}
