using Microsoft.OpenApi.Models;

namespace UserAuthenticationApi.Presentation.API.Extensions
{
    public static class ServiceExtension
    {
        public static void AddSwaggerExtension(this IServiceCollection service)
        {
            service.AddSwaggerGen(opt =>
            {
                List<string> xmlFiles = Directory.GetFiles
                (AppContext.BaseDirectory, "*.xml", searchOption: SearchOption.TopDirectoryOnly).ToList();
                xmlFiles.ForEach(xmlFile => opt.IncludeXmlComments(xmlFile));

                opt.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "UserAuthAPI",
                    Description = "API para autentificación de usuarios BHD",

                    Contact = new OpenApiContact
                    {
                        Name = "Felix Misae Mora",
                        Email = "misamora03@gmail.com",
                        Url = new Uri("https://www.instagram.com/misamorasuero/")
                    }
                });
            });
        }
    }
}
