using CMoussAdminUI.Models;

namespace CMoussAdminUI.Services;

public interface INotificationStore
{
    IReadOnlyList<Notification> GetNotifications(string userId);
    int GetUnreadCount(string userId);
    void AddNotification(Notification notification);
    void UpdateNotification(Notification notification);
    void RemoveNotification(string userId, string notificationId);
    void ClearAll(string userId);
}
