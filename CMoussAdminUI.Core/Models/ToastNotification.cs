namespace CMoussAdminUI.Models;

public class ToastNotification
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string? UserId { get; set; }
    public string Message { get; set; } = string.Empty;
    public NotificationType Type { get; set; } = NotificationType.Info;
    public int DurationMs { get; set; } = 5000;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
