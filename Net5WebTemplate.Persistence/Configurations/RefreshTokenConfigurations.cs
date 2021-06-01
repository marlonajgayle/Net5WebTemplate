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
                .IsRequired();

            builder.Property(e => e.Token)
                .IsRequired()
                .HasMaxLength(128);

            builder.Property(e => e.Invalidated)
                .IsRequired();

            builder.Property(e => e.Used)
                .IsRequired();

            builder.Property(e => e.UserId)
                .IsRequired()
                .HasMaxLength(64);
        }
    }
}
