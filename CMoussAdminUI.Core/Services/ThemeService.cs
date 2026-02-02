using Microsoft.Extensions.Options;

namespace CMoussAdminUI.Services;

/// <summary>
/// Provides access to theme configuration and generates CSS variables
/// </summary>
public class ThemeService : IThemeService
{
    private readonly CMoussAdminUIOptions _options;

    public ThemeService(IOptions<CMoussAdminUIOptions> options)
    {
        _options = options.Value;
    }

    public ThemeOptions Theme => _options.Theme;

    public string GenerateCssVariables()
    {
        var theme = _options.Theme;
        var primaryRgb = HexToRgb(theme.Colors.Primary);

        return $@"
:root {{
    --adminui-primary: {theme.Colors.Primary};
    --adminui-primary-rgb: {primaryRgb};
    --adminui-gradient-start: {theme.Colors.GradientStart};
    --adminui-gradient-end: {theme.Colors.GradientEnd};
    --adminui-topbar-bg: {theme.Topbar.Background};
    --adminui-topbar-text: {theme.Topbar.TextColor};
    --adminui-topbar-height: {theme.Topbar.Height}px;
    --adminui-sidebar-bg: {theme.Sidebar.Background};
    --adminui-sidebar-width: {theme.Sidebar.Width}px;
    --adminui-sidebar-link: {theme.Sidebar.LinkColor};
    --adminui-sidebar-link-active: {theme.Sidebar.LinkActiveColor};
}}
";
    }

    private static string HexToRgb(string hex)
    {
        hex = hex.TrimStart('#');
        if (hex.Length == 6)
        {
            try
            {
                var r = Convert.ToInt32(hex.Substring(0, 2), 16);
                var g = Convert.ToInt32(hex.Substring(2, 2), 16);
                var b = Convert.ToInt32(hex.Substring(4, 2), 16);
                return $"{r}, {g}, {b}";
            }
            catch
            {
                return "109, 40, 217"; // Default fallback
            }
        }
        return "109, 40, 217"; // Default fallback
    }
}
