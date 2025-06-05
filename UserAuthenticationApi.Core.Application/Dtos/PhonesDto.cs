namespace UserAuthenticationApi.Core.Application.Dtos
{
    public class PhonesDto
    {
        public int UserId { get; set; }
        public string Number { get; set; } = string.Empty;
        public int CityCode { get; set; }
        public string CountryCode { get; set; } = string.Empty;
    }
}
