namespace UserAuthenticationApi.Core.Domain.Abstraction
{
    public abstract class AuditableBaseEntity
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime ModifiedDate { get; set; } = DateTime.UtcNow;
    }
}
