namespace UserAuthenticationApi.Core.Domain.Entities
{
    public class Phone
    {
        public int Id { get; set; }
        public string Number { get; set; } = string.Empty;
        public int CityCode { get; set; }
        public string CountryCode { get; set; } = string.Empty;
            
        //Navigation Properties
        public Users Users { get; set; } = new();
    }
}
