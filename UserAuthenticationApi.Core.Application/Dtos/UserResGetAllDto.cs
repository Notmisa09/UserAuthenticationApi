namespace UserAuthenticationApi.Core.Application.Dtos
{
    public class UserResGetAllDto
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public IList<PhonesDto> Phones { get; set; } = [];
    }
}
