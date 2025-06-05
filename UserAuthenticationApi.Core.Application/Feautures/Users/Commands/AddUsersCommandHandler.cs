using AutoMapper;
using MediatR;
using UserAuthenticationApi.Core.Application.Common;
using UserAuthenticationApi.Core.Application.Helpers;
using UserAuthenticationApi.Core.Application.Interfaces.IRepositories;
using UserAuthenticationApi.Core.Domain.Entities;

namespace UserAuthenticationApi.Core.Application.Feautures.Users.Commands
{
    public class AddUsersCommandHandler : IRequestHandler<AddUsersCommand, Unit>
    {
        private readonly IUsersRepository _userRepository;
        public AddUsersCommandHandler(IUsersRepository userRepository) => _userRepository = userRepository;
        public async Task<Unit> Handle(AddUsersCommand command, CancellationToken cancellationToken)
        {
            var response = await _userRepository.EmailExistanceAsync(command.Email);
            if (response) throw new ApiExeption("El email ya está en uso");

            PasswordEncryptator.HashUserPassword384(command.Password);

            var adduser = new UserAuthenticationApi.Core.Domain.Entities.Users
            {
                IsActive = true,
                Email = command.Email,
                LastLogin = command.LastLogin,
                Name = command.Name,
                Password = command.Password,
            };

            await _userRepository.AddAsync(adduser);

            var phone = command.Phones
                .Select(x => new Phone
            {
                CityCode = x.CityCode,
                CountryCode = x.CountryCode,
                Number = x.Number,
                UserId = x.UserId,
            }).ToList();

            adduser.LastLogin = DateTime.UtcNow;
            adduser.Token = command.Token;
            return Unit.Value;
        }
    }
}
