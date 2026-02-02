using CMoussAdminUI.Models;

namespace CMoussAdminUI.Services;

public class NavigationService : INavigationService
{
    private readonly List<INavigationProvider> _providers = new();
    public event Action? OnNavigationChanged;

    public void RegisterProvider(INavigationProvider provider)
    {
        if (!_providers.Contains(provider))
        {
            _providers.Add(provider);
            _providers.Sort((a, b) => a.Priority.CompareTo(b.Priority));
            NotifyNavigationChanged();
        }
    }

    public async Task<IEnumerable<NavigationItem>> GetNavigationItemsAsync()
    {
        var allItems = new List<NavigationItem>();

        foreach (var provider in _providers)
        {
            var items = await provider.GetNavigationItemsAsync();
            allItems.AddRange(items);
        }

        return allItems
            .Where(item => item.Visible)
            .OrderBy(item => item.Order)
            .ToList();
    }

    public void NotifyNavigationChanged()
    {
        OnNavigationChanged?.Invoke();
    }
}
