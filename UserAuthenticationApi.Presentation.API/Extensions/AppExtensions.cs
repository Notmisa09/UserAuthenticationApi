using Swashbuckle.AspNetCore.SwaggerUI;
using UserAuthenticationApi.Presentation.API.Middleware;

namespace UserAuthenticationApi.Presentation.API.Extensions
{
    public static class AppExtensions
    {
        public static void UseSwaggerExtensions(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(opt =>
            {
                opt.SwaggerEndpoint("/swagger/v1/swagger.json,", "UserAuthApp");
                opt.DefaultModelRendering(ModelRendering.Model);
            });
        }
        public static void UseErrorHandleMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ErrorHandlerMiddleware>();
        }
    }
}
