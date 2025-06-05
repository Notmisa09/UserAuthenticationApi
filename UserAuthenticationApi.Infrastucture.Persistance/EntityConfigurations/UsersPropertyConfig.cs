using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserAuthenticationApi.Core.Domain.Entities;

namespace UserAuthenticationApi.Infrastucture.Persistance.EntityConfigurations
{
    public class UsersEntityConfig : IEntityTypeConfiguration<Users>
    {
        public void Configure(EntityTypeBuilder<Users> mb)
        {
            mb.ToTable("users");
            mb.HasKey(x => x.Id);
            mb.HasMany(x => x.Phones).
                WithOne(x => x.Users)
               .HasForeignKey(x => x.UserId)
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
