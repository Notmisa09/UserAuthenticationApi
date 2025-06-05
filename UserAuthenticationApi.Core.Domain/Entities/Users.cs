using UserAuthenticationApi.Core.Domain.Abstraction;

namespace UserAuthenticationApi.Core.Domain.Entities
{
    public class Users : AuditableBaseEntity
    {
        public string Token {  get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public IList<Phone> Phones { get; set; } = [];
    }
}
