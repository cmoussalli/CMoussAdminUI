using CMoussAdminUI.Models;

namespace CMoussAdminUI.Services;

public interface INotificationService
{
    event Action<string>? OnNotificationsChanged;
    event Action<ToastNotification>? OnToastNotification;

    IReadOnlyList<Notification> GetNotifications(string userId);
    int GetUnreadCount(string userId);

    void AddNotification(string userId, Notification notification);
    void AddNotification(string userId, string title, string message, NotificationType type = NotificationType.Info);
    void ShowToast(string userId, string message, NotificationType type = NotificationType.Info, int durationMs = 5000);
    void MarkAsRead(string userId, string notificationId);
    void MarkAllAsRead(string userId);
    void RemoveNotification(string userId, string notificationId);
    void ClearAll(string userId);
}
