using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserAuthenticationApi.Core.Domain.Entities;

namespace UserAuthenticationApi.Infrastucture.Persistance.EntityConfigurations
{
    internal class PhonePropertyConfig : IEntityTypeConfiguration<Phone>
    {
        public void Configure(EntityTypeBuilder<Phone> mb)
        {
            mb.HasKey(x => x.Id);

            mb.ToTable("phone");

            mb.Property(p => p.Number)
                .HasMaxLength(60)
                .IsRequired();

            mb.Property(p => p.CityCode)
                .HasMaxLength(60)
                .IsRequired();

            mb.Property(p => p.CountryCode)
                .HasMaxLength(60)
                .IsRequired();

            //Relaciones entre las entidades
            mb.HasOne(p => p.Users)
                .WithMany(p => p.Phones)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
