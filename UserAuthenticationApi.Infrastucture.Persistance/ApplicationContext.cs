using Microsoft.EntityFrameworkCore;
using UserAuthenticationApi.Core.Domain.Entities;
using UserAuthenticationApi.Infrastucture.Persistance.EntityConfigurations;

namespace UserAuthenticationApi.Infrastucture.Persistance
{
    public class ApplicationContext(DbContextOptions<ApplicationContext> options) : DbContext(options)
    {
        public DbSet<Users> Users { set; get; }
        public DbSet<Phone> Phones { set; get; }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.ApplyConfiguration(new UsersEntityConfig());
            base.OnModelCreating(mb);
        }
    }
}
