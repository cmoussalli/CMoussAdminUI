using System.Text.Json;
using CMoussAdminUI.Models;

namespace CMoussAdminUI.Data;

public class NotificationEntity
{
    public string Id { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public NotificationType Type { get; set; } = NotificationType.Info;
    public DateTime CreatedAt { get; set; }
    public bool IsRead { get; set; }
    public string? Icon { get; set; }
    public string? ActionUrl { get; set; }
    public string? ActionText { get; set; }
    public string? MetadataJson { get; set; }
    public int? DurationMs { get; set; }

    public Notification ToModel()
    {
        return new Notification
        {
            Id = Id,
            UserId = UserId,
            Title = Title,
            Message = Message,
            Type = Type,
            CreatedAt = CreatedAt,
            IsRead = IsRead,
            Icon = Icon,
            ActionUrl = ActionUrl,
            ActionText = ActionText,
            Metadata = DeserializeMetadata(MetadataJson),
            DurationMs = DurationMs
        };
    }

    public static NotificationEntity FromModel(Notification notification)
    {
        return new NotificationEntity
        {
            Id = notification.Id,
            UserId = notification.UserId ?? string.Empty,
            Title = notification.Title,
            Message = notification.Message,
            Type = notification.Type,
            CreatedAt = notification.CreatedAt,
            IsRead = notification.IsRead,
            Icon = notification.Icon,
            ActionUrl = notification.ActionUrl,
            ActionText = notification.ActionText,
            MetadataJson = SerializeMetadata(notification.Metadata),
            DurationMs = notification.DurationMs
        };
    }

    private static Dictionary<string, object>? DeserializeMetadata(string? metadataJson)
    {
        if (string.IsNullOrWhiteSpace(metadataJson))
        {
            return null;
        }

        try
        {
            return JsonSerializer.Deserialize<Dictionary<string, object>>(metadataJson);
        }
        catch (JsonException)
        {
            return null;
        }
    }

    private static string? SerializeMetadata(Dictionary<string, object>? metadata)
    {
        if (metadata == null || metadata.Count == 0)
        {
            return null;
        }

        return JsonSerializer.Serialize(metadata);
    }
}
