using CMoussAdminUI.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace CMoussAdminUI.Extensions;

public static class WebApplicationExtensions
{
    public static WebApplication UseCMoussAdminUI(this WebApplication app)
    {
        // Auto-register navigation provider if one was registered
        var navProvider = app.Services.GetService<INavigationProvider>();
        if (navProvider != null)
        {
            var navService = app.Services.GetRequiredService<INavigationService>();
            navService.RegisterProvider(navProvider);
        }

        return app;
    }
}
