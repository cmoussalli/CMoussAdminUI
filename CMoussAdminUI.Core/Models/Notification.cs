namespace CMoussAdminUI.Models;

public class Notification
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string? UserId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public NotificationType Type { get; set; } = NotificationType.Info;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public bool IsRead { get; set; }
    public string? Icon { get; set; }
    public string? ActionUrl { get; set; }
    public string? ActionText { get; set; }
    public Dictionary<string, object>? Metadata { get; set; }
    public int? DurationMs { get; set; }
}
