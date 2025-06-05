namespace UserAuthenticationApi.Core.Application.Dtos
{
    public class UserGetAllDto
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public IList<PhonesDto> Phones { get; set; } = [];
    }
}
