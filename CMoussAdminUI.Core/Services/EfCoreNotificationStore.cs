using System.Reflection;
using CMoussAdminUI.Data;
using CMoussAdminUI.Models;
using Microsoft.EntityFrameworkCore;

namespace CMoussAdminUI.Services;

public class EfCoreNotificationStore : INotificationStore
{
    private static readonly object InitLock = new();
    private static bool _initialized;

    private static void EnsureInitialized()
    {
        lock (InitLock)
        {
            if (_initialized)
            {
                return;
            }

            using var db = new CMoussAdminUIDbContext();
            db.Database.EnsureCreated();

            var refreshMethod = db.GetType().GetMethod(
                "RefreshDBContext",
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            refreshMethod?.Invoke(db, null);

            _initialized = true;
        }
    }

    public IReadOnlyList<Notification> GetNotifications(string userId)
    {
        EnsureInitialized();
        using var db = new CMoussAdminUIDbContext();
        return db.Notifications
            .AsNoTracking()
            .Where(n => n.UserId == userId)
            .Select(n => n.ToModel())
            .ToList();
    }

    public int GetUnreadCount(string userId)
    {
        EnsureInitialized();
        using var db = new CMoussAdminUIDbContext();
        return db.Notifications
            .AsNoTracking()
            .Count(n => n.UserId == userId && !n.IsRead);
    }

    public void AddNotification(Notification notification)
    {
        EnsureInitialized();
        using var db = new CMoussAdminUIDbContext();
        db.Notifications.Add(NotificationEntity.FromModel(notification));
        db.SaveChanges();
    }

    public void UpdateNotification(Notification notification)
    {
        EnsureInitialized();
        using var db = new CMoussAdminUIDbContext();
        db.Notifications.Update(NotificationEntity.FromModel(notification));
        db.SaveChanges();
    }

    public void RemoveNotification(string userId, string notificationId)
    {
        EnsureInitialized();
        using var db = new CMoussAdminUIDbContext();
        var notification = db.Notifications.FirstOrDefault(n => n.UserId == userId && n.Id == notificationId);
        if (notification != null)
        {
            db.Notifications.Remove(notification);
            db.SaveChanges();
        }
    }

    public void ClearAll(string userId)
    {
        EnsureInitialized();
        using var db = new CMoussAdminUIDbContext();
        var notifications = db.Notifications.Where(n => n.UserId == userId).ToList();
        if (notifications.Count > 0)
        {
            db.Notifications.RemoveRange(notifications);
            db.SaveChanges();
        }
    }
}
