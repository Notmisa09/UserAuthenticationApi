namespace UserAuthenticationApi.Core.Application.Common
{
    public class ApiResponse
    {
        public string Message { get; set; }
        public ApiResponse(string message) { Message = message; }    
    }
}
