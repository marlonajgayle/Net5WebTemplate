using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Net5WebTemplate.Domain.Entities;

namespace Net5WebTemplate.Persistence.Configurations
{
    public class ProfileConfigurations : IEntityTypeConfiguration<Profile>
    {
        public void Configure(EntityTypeBuilder<Profile> builder)
        {
            builder.Property(e => e.Id)
                .HasColumnName("ProfileID");

            builder.Property(e => e.FirstName)
                .HasMaxLength(100)
                .IsRequired(true)
                .IsUnicode(false);

            builder.Property(e => e.LastName)
               .HasMaxLength(100)
               .IsRequired(true)
               .IsUnicode(false);

            builder.Property(e => e.PhoneNumber)
                .HasMaxLength(11)
                .IsUnicode(false);

            builder.OwnsOne(p => p.Address, a => 
            {
                a.Property(a => a.AddressLine1)
                .HasMaxLength(100)
                .IsRequired(true)
                .IsUnicode(false);

                a.Property(a => a.AddressLine2)
                .HasMaxLength(100)
                .HasDefaultValue("")
                .IsUnicode(false);

                a.Property(a => a.Parish)
                .HasMaxLength(50)
                .IsRequired(true)
                .IsUnicode(false);
            });
        }
    }
}
