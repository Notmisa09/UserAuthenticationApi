namespace UserAuthenticationApi.Core.Application.Common
{
    public class ApiExeption : Exception
    {
        public ApiExeption() {}
        public ApiExeption(string message) : base(message) {}
    }
}
