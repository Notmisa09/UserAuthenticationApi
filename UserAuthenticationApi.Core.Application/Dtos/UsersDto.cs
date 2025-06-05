namespace UserAuthenticationApi.Core.Application.Dtos
{
    public class UsersDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public DateTime? LastLogin { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string Token { get; set; } = string.Empty;
        public IList<PhonesDto> Phones { get; set; } = [];
    }
}
