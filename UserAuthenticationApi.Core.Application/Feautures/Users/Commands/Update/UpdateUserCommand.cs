using MediatR;
using System.ComponentModel.DataAnnotations;

namespace UserAuthenticationApi.Core.Application.Feautures.Users.Commands.Update
{
    public class UpdateUserCommand : IRequest<Unit>
    {
        [Required]
        public Guid Id { get; set; }
        public string? Name { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        public string? Password { get; set; } = string.Empty;

    }
}
