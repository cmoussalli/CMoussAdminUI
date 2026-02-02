using CMoussAdminUI.Models;

namespace CMoussAdminUI.Services;

public interface INavigationProvider
{
    Task<IEnumerable<NavigationItem>> GetNavigationItemsAsync();
    int Priority { get; }
}
