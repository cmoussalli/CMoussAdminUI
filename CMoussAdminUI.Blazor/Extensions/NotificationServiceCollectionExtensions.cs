using CMoussAdminUI.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CMoussAdminUI.Extensions;

public static class NotificationServiceCollectionExtensions
{
    /// <summary>
    /// Enables database-backed notification storage using EF Core.
    /// </summary>
    /// <remarks>
    /// Consider using <c>options.UseNotificationDb = true</c> in AddCMoussAdminUI() instead.
    /// </remarks>
    [Obsolete("Use options.UseNotificationDb = true in AddCMoussAdminUI() instead. This method will be removed in a future version.")]
    public static IServiceCollection AddCMoussAdminUINotificationDb(this IServiceCollection services)
    {
        services.AddSingleton<INotificationStore, EfCoreNotificationStore>();
        return services;
    }
}
