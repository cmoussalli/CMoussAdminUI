using CMoussAdminUI.Models;

namespace CMoussAdminUI.Services;

public interface INavigationService
{
    Task<IEnumerable<NavigationItem>> GetNavigationItemsAsync();
    void RegisterProvider(INavigationProvider provider);
    event Action? OnNavigationChanged;
    void NotifyNavigationChanged();
}
