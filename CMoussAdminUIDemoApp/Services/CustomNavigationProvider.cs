using CMoussAdminUI.Models;
using CMoussAdminUI.Services;

namespace CMoussAdminUIDemoApp.Services;

public class CustomNavigationProvider : INavigationProvider
{
    public int Priority => 0;

    public Task<IEnumerable<NavigationItem>> GetNavigationItemsAsync()
    {
        IEnumerable<NavigationItem> items = new List<NavigationItem>
        {
            new NavigationItem
            {
                Id = "custom-dashboard",
                Text = "Custom Dashboard",
                Url = "/custom-dashboard",
                Icon = "dashboard",
                Order = 1,
                Visible = true
            },
            new NavigationItem
            {
                Id = "notifications",
                Text = "Notifications + Toasts",
                Url = "/notifications",
                Icon = "bar_chart",
                Order = 2,
                Visible = true
            }
        };

        return Task.FromResult<IEnumerable<NavigationItem>>(items);
    }
}
