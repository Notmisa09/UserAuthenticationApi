namespace UserAuthenticationApi.Core.Application.Dtos
{
    public class UserResAddDto
    {
        public Guid Id { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime? LastLogin { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string Token { get; set; } = string.Empty;
    }
}
