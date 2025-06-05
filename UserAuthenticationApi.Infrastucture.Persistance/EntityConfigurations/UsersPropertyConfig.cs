using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserAuthenticationApi.Core.Domain.Entities;

namespace UserAuthenticationApi.Infrastucture.Persistance.EntityConfigurations
{
    public class UsersEntityConfig : IEntityTypeConfiguration<Users>
    {
        public void Configure(EntityTypeBuilder<Users> mb)
        {
            mb.HasKey(x => x.Id);
          
            mb.ToTable("users");

            mb.Property(u => u.Name)
                .HasMaxLength(50)
                .IsRequired();

            mb.Property(u => u.Email)
                .HasMaxLength(50)
                .IsRequired();

            mb.Property(u => u.Password)
                .HasMaxLength(225)
                .IsRequired();

            mb.Property(u => u.CreatedDate)
                .HasDefaultValue("GETUTCDATE()")
                .IsRequired();

            mb.Property(u => u.ModifiedDate)
                .IsRequired(false);

            mb.Property(u => u.LastLogin)
                .HasDefaultValue("GETUTCDATE()")
                .IsRequired(false);
        }
    }
}
