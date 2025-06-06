using MediatR;
using UserAuthenticationApi.Core.Application.Dtos;

namespace UserAuthenticationApi.Core.Application.Feautures.Users.Commands.Update
{
    public class UpdateCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
        public string? Name { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        public string? Password { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public IList<PhonesDto> Phones { get; set; } = [];
    }
}
