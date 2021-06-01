using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Net5WebTemplate.Domain.Entities;

namespace Net5WebTemplate.Persistence.Configurations
{
    public class LoginAuditLogConfigurations : IEntityTypeConfiguration<LoginAuditLog>
    {
        public void Configure(EntityTypeBuilder<LoginAuditLog> builder)
        {
            builder.Property(e => e.Description)
                .HasMaxLength(100)
                .IsRequired()
                .IsUnicode(false);

            builder.Property(e => e.IpAddress)
                .HasMaxLength(32)
                .IsUnicode(false);

            builder.Property(e => e.IsSuccess)
                .IsRequired();

            builder.Property(e => e.Username)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(e => e.Timestamp)
                .IsRequired();
        }
    }
}
