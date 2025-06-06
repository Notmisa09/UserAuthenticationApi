using System.Net;
using System.Text.Json;
using UserAuthenticationApi.Core.Application.Common;

namespace UserAuthenticationApi.Presentation.API.Middleware
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception error)
            {
                var response = httpContext.Response;
                response.ContentType = "application/json";
                var responseModel = new { mensaje =  error.Message };
                response.StatusCode = error switch
                {
                    ApiException e =>

                        e.ErrorCode switch
                        {
                            (int)HttpStatusCode.BadRequest => (int)HttpStatusCode.BadRequest,
                            (int)HttpStatusCode.InternalServerError => (int)HttpStatusCode.InternalServerError,
                            (int)HttpStatusCode.NotFound => (int)HttpStatusCode.NotFound,
                            (int)HttpStatusCode.NoContent => (int)HttpStatusCode.NoContent,
                            _ => (int)HttpStatusCode.InternalServerError
                        },
                    KeyNotFoundException e =>

                        (int)HttpStatusCode.NotFound,
                    _ => (int)HttpStatusCode.InternalServerError
                };

                var result = JsonSerializer.Serialize(responseModel);

                await response.WriteAsync(result);
            }
        }

    }
}
