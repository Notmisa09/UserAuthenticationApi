using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace UserAuthenticationApi.Infrastucture.Persistance
{
    public static class ServiceRegisration
    {
        public static void AddPersistanceLayer(this IServiceCollection service, IConfiguration config)
        {
            service.AddDbContext<ApplicationContext>(
                x => x.UseSqlServer(config.GetConnectionString("DefaultConnection")
                , m => m.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName)));
        }
    }
}
