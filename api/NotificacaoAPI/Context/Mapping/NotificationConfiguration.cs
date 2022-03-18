using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NotificacaoAPI.Model;

namespace NotificacaoAPI.Context.Mapping
{
    public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.ToTable("notifications");
            builder.HasKey(t => t.Id);

            builder.Property(t=> t.Id).IsRequired();
            builder.Property(t=> t.IssueDate).IsRequired();
            builder.Property(t => t.Sender).IsRequired().HasMaxLength(50);
            builder.Property(t => t.Destination).IsRequired().HasMaxLength(50);
            builder.Property(t => t.Message).IsRequired().HasMaxLength(100);
            builder.Property(t => t.UrlRedirect).HasMaxLength(500);
            builder.Property(t=> t.IsRead).IsRequired().HasDefaultValue(false);
        }
    }
}
