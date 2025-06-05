using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserAuthenticationApi.Core.Application.Interfaces.IRepositories;
using UserAuthenticationApi.Infrastucture.Persistance.Repositories;

namespace UserAuthenticationApi.Infrastucture.Persistance
{
    public static class ServiceRegisration
    {
        public static void AddPersistanceLayer(this IServiceCollection service, IConfiguration config)
        {
            #region Database Connection
            service.AddDbContext<ApplicationContext>(
                x => x.UseSqlServer(config.GetConnectionString("DefaultConnection")
                , m => m.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName)));
            #endregion

            #region Injections
            service.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            service.AddTransient<IUsersRepository , UsersRepository>();
            service.AddTransient<IPhoneRepository, PhoneRepository>();
            #endregion
        }
    }
}
