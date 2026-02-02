using CMoussAdminUI.Models;

namespace CMoussAdminUI.Services;

public class NotificationService : INotificationService
{
    private readonly INotificationStore _notificationStore;
    private readonly object _lock = new();

    public event Action<string>? OnNotificationsChanged;
    public event Action<ToastNotification>? OnToastNotification;

    public NotificationService(INotificationStore notificationStore)
    {
        _notificationStore = notificationStore;
    }

    public IReadOnlyList<Notification> GetNotifications(string userId)
    {
        lock (_lock)
        {
            return _notificationStore
                .GetNotifications(userId)
                .OrderByDescending(n => n.CreatedAt)
                .ToList();
        }
    }

    public int GetUnreadCount(string userId)
    {
        lock (_lock)
        {
            return _notificationStore.GetUnreadCount(userId);
        }
    }

    public void AddNotification(string userId, Notification notification)
    {
        notification.UserId = userId;

        lock (_lock)
        {
            _notificationStore.AddNotification(notification);
        }

        OnNotificationsChanged?.Invoke(userId);

        if (notification.DurationMs.HasValue)
        {
            var toast = new ToastNotification
            {
                Id = notification.Id,
                UserId = userId,
                Message = notification.Message,
                Type = notification.Type,
                DurationMs = notification.DurationMs.Value,
                CreatedAt = notification.CreatedAt
            };
            OnToastNotification?.Invoke(toast);
        }
    }

    public void AddNotification(string userId, string title, string message, NotificationType type = NotificationType.Info)
    {
        var notification = new Notification
        {
            UserId = userId,
            Title = title,
            Message = message,
            Type = type
        };
        AddNotification(userId, notification);
    }

    public void ShowToast(string userId, string message, NotificationType type = NotificationType.Info, int durationMs = 5000)
    {
        var toast = new ToastNotification
        {
            UserId = userId,
            Message = message,
            Type = type,
            DurationMs = durationMs
        };
        OnToastNotification?.Invoke(toast);
    }

    public void MarkAsRead(string userId, string notificationId)
    {
        lock (_lock)
        {
            var notifications = _notificationStore.GetNotifications(userId);
            var notification = notifications.FirstOrDefault(n => n.Id == notificationId);
            if (notification != null && !notification.IsRead)
            {
                notification.IsRead = true;
                _notificationStore.UpdateNotification(notification);
                OnNotificationsChanged?.Invoke(userId);
            }
        }
    }

    public void MarkAllAsRead(string userId)
    {
        lock (_lock)
        {
            var notifications = _notificationStore.GetNotifications(userId);
            var hasUnread = notifications.Any(n => !n.IsRead);
            if (hasUnread)
            {
                foreach (var notification in notifications)
                {
                    notification.IsRead = true;
                    _notificationStore.UpdateNotification(notification);
                }
                OnNotificationsChanged?.Invoke(userId);
            }
        }
    }

    public void RemoveNotification(string userId, string notificationId)
    {
        lock (_lock)
        {
            _notificationStore.RemoveNotification(userId, notificationId);
            OnNotificationsChanged?.Invoke(userId);
        }
    }

    public void ClearAll(string userId)
    {
        lock (_lock)
        {
            _notificationStore.ClearAll(userId);
            OnNotificationsChanged?.Invoke(userId);
        }
    }
}
