namespace UserAuthenticationApi.Core.Application.Common
{
    public class ApiExeption : Exception
    {
        public int ErrorCode { set; get; }
        public ApiExeption() {}
        public ApiExeption(string message) : base(message) {}
        public ApiExeption(string message, int errorcode) : base(message) { ErrorCode = errorcode; }

    }
}
