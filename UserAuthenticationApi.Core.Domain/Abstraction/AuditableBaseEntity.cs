namespace UserAuthenticationApi.Core.Domain.Abstraction
{
    public abstract class AuditableBaseEntity
    {
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
