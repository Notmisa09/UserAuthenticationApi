using Swashbuckle.AspNetCore.SwaggerUI;

namespace UserAuthenticationApi.Presentation.API.Extensions
{
    public static class AppExtensions
    {
        public static void UserSwaggerExtensions(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(opt =>
            {
                opt.SwaggerEndpoint("/swagger/v1/swagger.json,", "UserAuthApp");
                opt.DefaultModelRendering(ModelRendering.Model);
            });
        }
    }
}
