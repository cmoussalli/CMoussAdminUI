using CMoussAdminUI.Models;

namespace CMoussAdminUI.Services;

public class InMemoryNotificationStore : INotificationStore
{
    private readonly Dictionary<string, List<Notification>> _userNotifications = new();
    private readonly object _lock = new();

    public IReadOnlyList<Notification> GetNotifications(string userId)
    {
        lock (_lock)
        {
            if (_userNotifications.TryGetValue(userId, out var notifications))
            {
                return notifications.ToList();
            }
            return new List<Notification>();
        }
    }

    public int GetUnreadCount(string userId)
    {
        lock (_lock)
        {
            if (_userNotifications.TryGetValue(userId, out var notifications))
            {
                return notifications.Count(n => !n.IsRead);
            }
            return 0;
        }
    }

    public void AddNotification(Notification notification)
    {
        lock (_lock)
        {
            var userId = notification.UserId ?? string.Empty;
            if (!_userNotifications.ContainsKey(userId))
            {
                _userNotifications[userId] = new List<Notification>();
            }
            _userNotifications[userId].Add(notification);
        }
    }

    public void UpdateNotification(Notification notification)
    {
        lock (_lock)
        {
            var userId = notification.UserId ?? string.Empty;
            if (_userNotifications.TryGetValue(userId, out var notifications))
            {
                var index = notifications.FindIndex(n => n.Id == notification.Id);
                if (index >= 0)
                {
                    notifications[index] = notification;
                }
            }
        }
    }

    public void RemoveNotification(string userId, string notificationId)
    {
        lock (_lock)
        {
            if (_userNotifications.TryGetValue(userId, out var notifications))
            {
                var notification = notifications.FirstOrDefault(n => n.Id == notificationId);
                if (notification != null)
                {
                    notifications.Remove(notification);
                }
            }
        }
    }

    public void ClearAll(string userId)
    {
        lock (_lock)
        {
            if (_userNotifications.TryGetValue(userId, out var notifications))
            {
                notifications.Clear();
            }
        }
    }
}
