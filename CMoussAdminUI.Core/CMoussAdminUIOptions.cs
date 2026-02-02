using Microsoft.Extensions.DependencyInjection;

namespace CMoussAdminUI;

public class CMoussAdminUIOptions
{
    /// <summary>
    /// The type of INavigationProvider to register. If null, no provider is registered.
    /// </summary>
    public Type? NavigationProviderType { get; set; }

    /// <summary>
    /// The service lifetime for the navigation provider (default: Singleton)
    /// </summary>
    public ServiceLifetime NavigationProviderLifetime { get; set; } = ServiceLifetime.Singleton;

    /// <summary>
    /// Whether to add IHttpContextAccessor (default: true)
    /// </summary>
    public bool AddHttpContextAccessor { get; set; } = true;

    /// <summary>
    /// Whether to use database-backed notification storage (EF Core) instead of in-memory storage.
    /// When true, notifications persist across application restarts. (default: false)
    /// </summary>
    public bool UseNotificationDb { get; set; } = false;

    /// <summary>
    /// Theme and branding configuration
    /// </summary>
    public ThemeOptions Theme { get; set; } = new();
}

/// <summary>
/// Configuration options for UI theming and branding
/// </summary>
public class ThemeOptions
{
    /// <summary>
    /// Application name displayed in topbar and sidebar brand
    /// </summary>
    public string ApplicationName { get; set; } = "Admin Panel";

    /// <summary>
    /// Default browser tab title (pages can override with PageTitle component)
    /// </summary>
    public string DefaultPageTitle { get; set; } = "Admin Panel";

    /// <summary>
    /// Topbar configuration
    /// </summary>
    public TopbarOptions Topbar { get; set; } = new();

    /// <summary>
    /// Sidebar configuration
    /// </summary>
    public SidebarOptions Sidebar { get; set; } = new();

    /// <summary>
    /// Color theme configuration
    /// </summary>
    public ColorOptions Colors { get; set; } = new();

    /// <summary>
    /// Logo configuration
    /// </summary>
    public LogoOptions Logo { get; set; } = new();
}

/// <summary>
/// Topbar styling options
/// </summary>
public class TopbarOptions
{
    /// <summary>
    /// CSS background for topbar (supports gradients, solid colors, etc.)
    /// </summary>
    public string Background { get; set; } = "linear-gradient(135deg, #667eea 0%, #764ba2 100%)";

    /// <summary>
    /// Text color for topbar elements
    /// </summary>
    public string TextColor { get; set; } = "#ffffff";

    /// <summary>
    /// Height of the topbar in pixels
    /// </summary>
    public int Height { get; set; } = 60;
}

/// <summary>
/// Sidebar styling options
/// </summary>
public class SidebarOptions
{
    /// <summary>
    /// Sidebar background color
    /// </summary>
    public string Background { get; set; } = "#171332";

    /// <summary>
    /// Sidebar width in pixels
    /// </summary>
    public int Width { get; set; } = 250;

    /// <summary>
    /// Navigation link text color
    /// </summary>
    public string LinkColor { get; set; } = "rgba(255, 255, 255, 0.8)";

    /// <summary>
    /// Navigation link hover/active color
    /// </summary>
    public string LinkActiveColor { get; set; } = "#ffffff";
}

/// <summary>
/// Color theme options
/// </summary>
public class ColorOptions
{
    /// <summary>
    /// Primary brand color (hex format)
    /// </summary>
    public string Primary { get; set; } = "#6d28d9";

    /// <summary>
    /// Start color for gradient backgrounds
    /// </summary>
    public string GradientStart { get; set; } = "#667eea";

    /// <summary>
    /// End color for gradient backgrounds
    /// </summary>
    public string GradientEnd { get; set; } = "#764ba2";
}

/// <summary>
/// Logo and brand icon options
/// </summary>
public class LogoOptions
{
    /// <summary>
    /// URL to logo image (optional, uses icon if not set)
    /// </summary>
    public string? ImageUrl { get; set; }

    /// <summary>
    /// Bootstrap icon class for brand icon (used when ImageUrl is null)
    /// </summary>
    public string IconClass { get; set; } = "bi-speedometer2";

    /// <summary>
    /// Alt text for logo image
    /// </summary>
    public string AltText { get; set; } = "Logo";
}
