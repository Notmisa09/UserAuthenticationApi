using MediatR;
using UserAuthenticationApi.Core.Application.Common;
using UserAuthenticationApi.Core.Application.Interfaces.IRepositories;

namespace UserAuthenticationApi.Core.Application.Feautures.Users.Commands.Delete
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Unit>
    {
        private readonly IUsersRepository _userRepository;
        public DeleteUserCommandHandler(
            IUsersRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<Unit> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(command.Id);
            if (user == null) throw new ApiExeption("La entidad que estás intentando eliminar no ha podido ser encontrada.",404);

            user.ModifiedDate = DateTime.Now;
            user.IsActive = false;

            await _userRepository.UpdateAsync(user, command.Id);
            return Unit.Value;  
        }
    }
}
