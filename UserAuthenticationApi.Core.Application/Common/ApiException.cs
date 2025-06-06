namespace UserAuthenticationApi.Core.Application.Common
{
    public class ApiException : Exception
    {
        public int ErrorCode { set; get; }
        public ApiException(string message, int errorcode) : base(message) => ErrorCode = errorcode;

    }
}
