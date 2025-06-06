namespace UserAuthenticationApi.Core.Application.Dtos
{
    public class PhoneReqAddDto
    {
        public string Number { get; set; } = string.Empty;
        public int CityCode { get; set; }
        public string CountryCode { get; set; } = string.Empty;
    }
}
