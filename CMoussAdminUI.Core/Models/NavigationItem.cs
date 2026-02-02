namespace CMoussAdminUI.Models;

public class NavigationItem
{
    public string Id { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
    public string? IconClass { get; set; }
    public int Order { get; set; }
    public bool Visible { get; set; } = true;
    public List<string>? RequiredRoles { get; set; }
    public List<string>? RequiredPermissions { get; set; }
    public List<NavigationItem>? Children { get; set; }
    public Dictionary<string, object>? Metadata { get; set; }
}
