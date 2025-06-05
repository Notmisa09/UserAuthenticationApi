using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using UserAuthenticationApi.Core.Application.Interfaces.IServices;
using UserAuthenticationApi.Core.Application.Services;
using UserAuthenticationApi.Core.Domain.Settings;

namespace UserAuthenticationApi.Core.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationLayer(this IServiceCollection service, IConfiguration config)
        {
            #region Dependencies
            service.AddMediatR(config => config.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            service.AddAutoMapper(Assembly.GetExecutingAssembly());
            service.AddSingleton<IJwtGeneratorService, JwtGeneratorService>();
            #endregion

            #region JWTConfigurations

            service.Configure<JWTSettings>(config.GetSection("JWTSettings"));
            service.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            });

            #endregion
        }
    }
}
