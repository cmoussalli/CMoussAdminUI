namespace CMoussAdminUI.Services;

/// <summary>
/// Provides access to theme configuration throughout the application
/// </summary>
public interface IThemeService
{
    /// <summary>
    /// Gets the current theme options
    /// </summary>
    ThemeOptions Theme { get; }

    /// <summary>
    /// Generates CSS variable declarations from the theme options
    /// </summary>
    string GenerateCssVariables();
}
