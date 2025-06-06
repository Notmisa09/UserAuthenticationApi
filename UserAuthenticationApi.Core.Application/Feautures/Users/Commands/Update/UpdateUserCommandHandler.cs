using AutoMapper;
using MediatR;
using UserAuthenticationApi.Core.Application.Common;
using UserAuthenticationApi.Core.Application.Helpers;
using UserAuthenticationApi.Core.Application.Interfaces.IRepositories;
using UserAuthenticationApi.Core.Application.Interfaces.IServices;

namespace UserAuthenticationApi.Core.Application.Feautures.Users.Commands.Update
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Unit>
    {
        private readonly IUsersRepository _userRepository;
        private readonly IJwtGeneratorService _jwtGeneratorService;
        private readonly IMapper _mapper;

        public UpdateUserCommandHandler(
            IUsersRepository userRepository,
            IJwtGeneratorService jwtGeneratorService,
            IMapper mapper)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _jwtGeneratorService = jwtGeneratorService;
        }

        public async Task<Unit> Handle(UpdateUserCommand commnad, CancellationToken cancellationToken)
        {
            var user  = await _userRepository.GetByIdAsync(commnad.Id);
            if (user == null) throw new ApiException($"No se pudo encontrar una entidad bajo este id {commnad.Id}",404);

            user.Email = string.IsNullOrEmpty(commnad.Email) ? user.Email : commnad.Email;
            user.Name = string.IsNullOrEmpty(commnad.Name) ? user.Name : commnad.Name;
            user.Password = string.IsNullOrEmpty(commnad.Password) ? user.Password : commnad.Password;

            if (string.IsNullOrEmpty(commnad.Name))
            {
                
            }
            else
            {
                user.Name = commnad.Name;
                user.Password = PasswordEncryptator.HashUserPassword384(user.Password);
            }

            user.ModifiedDate = DateTime.Now;
            await _userRepository.UpdateAsync(user, user.Id);
            return Unit.Value;
        }
    }
}
