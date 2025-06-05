using UserAuthenticationApi.Core.Domain.Abstraction;

namespace UserAuthenticationApi.Core.Domain.Entities
{
    public class Users : AuditableBaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public DateTime? LastLogin {  get; set; }
        public string Token {  get; set; } = string.Empty;

        //Navigation Properties
        public Guid Id { get; set; }
        public IList<Phone> Phones { get; set; } = [];
    }
}
