using FluentValidation;
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

                string message = error.Message;
                int statusCode;

                switch (error)
                {
                    case ValidationException validationException:
                        statusCode = (int)HttpStatusCode.BadRequest;
                        message = string.Join(" | ", validationException.Errors.Select(e => e.ErrorMessage));
                        break;
                    case ApiException apiException:
                        statusCode = apiException.ErrorCode switch
                        {
                            (int)HttpStatusCode.BadRequest => (int)HttpStatusCode.BadRequest,
                            (int)HttpStatusCode.InternalServerError => (int)HttpStatusCode.InternalServerError,
                            (int)HttpStatusCode.NotFound => (int)HttpStatusCode.NotFound,
                            (int)HttpStatusCode.NoContent => (int)HttpStatusCode.NoContent,
                            _ => (int)HttpStatusCode.InternalServerError
                        };
                        break;
                    case KeyNotFoundException:
                        statusCode = (int)HttpStatusCode.NotFound;
                        break;
                    default:
                        statusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                response.StatusCode = statusCode;
                var responseModel = new { mensaje = message };
                var result = JsonSerializer.Serialize(responseModel);

                await response.WriteAsync(result);
            }
        }
    }
}
