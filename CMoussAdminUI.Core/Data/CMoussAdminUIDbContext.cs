using CMouss.IdentityFramework;
using Microsoft.EntityFrameworkCore;

namespace CMoussAdminUI.Data;

public class CMoussAdminUIDbContext : IDFDBContext
{
    public DbSet<NotificationEntity> Notifications { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var entity = modelBuilder.Entity<NotificationEntity>();
        entity.HasKey(n => n.Id);
        entity.Property(n => n.UserId).IsRequired();
        entity.HasIndex(n => n.UserId);
        entity.HasIndex(n => new { n.UserId, n.IsRead });
        entity.HasIndex(n => new { n.UserId, n.CreatedAt });
        entity.Property(n => n.Title).HasMaxLength(256);
        entity.Property(n => n.Message).HasMaxLength(2048);
        entity.Property(n => n.Icon).HasMaxLength(128);
        entity.Property(n => n.ActionUrl).HasMaxLength(1024);
        entity.Property(n => n.ActionText).HasMaxLength(256);
    }
}
