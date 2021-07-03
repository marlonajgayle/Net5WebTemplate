using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Net5WebTemplate.Domain.Entities;

namespace Net5WebTemplate.Persistence.Configurations
{
    public class RefreshTokenConfigurations : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.HasKey(e => e.JwtId);

            builder.Property(e => e.JwtId)
                .HasMaxLength(128)
                .IsRequired();

            builder.Property(e => e.Token)
                .HasMaxLength(128)
                .IsRequired();

            builder.Property(e => e.CreationDate)
                .IsRequired();

            builder.Property(e => e.ExpirationDate)
                .IsRequired();

            builder.Property(e => e.Revoked);

            builder.Property(e => e.RemoteIpAddress)
                .HasMaxLength(128);
        }
    }
}
