using CMoussAdminUI.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CMoussAdminUI.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCMoussAdminUI(this IServiceCollection services)
    {
        return services.AddCMoussAdminUI(_ => { });
    }

    public static IServiceCollection AddCMoussAdminUI(
        this IServiceCollection services,
        Action<CMoussAdminUIOptions> configure)
    {
        var options = new CMoussAdminUIOptions();
        configure(options);

        // Register options for IOptions<CMoussAdminUIOptions> access
        services.Configure<CMoussAdminUIOptions>(opt =>
        {
            opt.NavigationProviderType = options.NavigationProviderType;
            opt.NavigationProviderLifetime = options.NavigationProviderLifetime;
            opt.AddHttpContextAccessor = options.AddHttpContextAccessor;
            opt.UseNotificationDb = options.UseNotificationDb;
            opt.Theme = options.Theme;
        });

        // Core services
        services.AddSingleton<INavigationService, NavigationService>();
        services.AddSingleton<INotificationService, NotificationService>();

        // Notification storage: EF Core database or in-memory
        if (options.UseNotificationDb)
        {
            services.AddSingleton<INotificationStore, EfCoreNotificationStore>();
        }
        else
        {
            services.TryAddSingleton<INotificationStore, InMemoryNotificationStore>();
        }

        // Theme service
        services.AddSingleton<IThemeService, ThemeService>();

        // Optional: HttpContextAccessor
        if (options.AddHttpContextAccessor)
        {
            services.AddHttpContextAccessor();
        }

        // Optional: Navigation provider
        if (options.NavigationProviderType != null)
        {
            services.Add(new ServiceDescriptor(
                typeof(INavigationProvider),
                options.NavigationProviderType,
                options.NavigationProviderLifetime));
        }

        return services;
    }
}
