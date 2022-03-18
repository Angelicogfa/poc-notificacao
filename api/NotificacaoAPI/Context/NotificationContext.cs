using Microsoft.EntityFrameworkCore;
using NotificacaoAPI.Context.Mapping;
using NotificacaoAPI.Model;

namespace NotificacaoAPI.Context
{
    public class NotificationContext : DbContext
    {
        public NotificationContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Notification> Notifications { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new NotificationConfiguration());
        }
    }
}
