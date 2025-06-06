using MediatR;
using System.ComponentModel.DataAnnotations;

namespace UserAuthenticationApi.Core.Application.Feautures.Users.Commands.Delete
{
    public class DeleteUserCommand : IRequest<Unit>
    {
        [Required(ErrorMessage = "Coloque un id a eliminar")]
        public Guid Id { get; set; }
    }
}
