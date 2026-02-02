using CMoussAdminUI.Models;
using CMoussAdminUI.Services;

namespace CMoussAdminUIDemoApp.Services;

public class StaticNavigationProvider : INavigationProvider
{
    public int Priority => 0;

    public Task<IEnumerable<NavigationItem>> GetNavigationItemsAsync()
    {
        var navigationItems = new List<NavigationItem>
        {
            new NavigationItem
            {
                Id = "dashboard",
                Text = "Dashboard",
                Url = "/",
                Icon = "house-door",
                Order = 1,
                Visible = true
            },
            //new NavigationItem
            //{
            //    Id = "users",
            //    Text = "Users",
            //    Url = "/users",
            //    Icon = "people",
            //    Order = 2,
            //    Visible = true
            //},
            //new NavigationItem
            //{
            //    Id = "analytics",
            //    Text = "Analytics",
            //    Url = "/analytics",
            //    Icon = "graph-up",
            //    Order = 3,
            //    Visible = true
            //},
            //new NavigationItem
            //{
            //    Id = "settings",
            //    Text = "Settings",
            //    Url = "/settings",
            //    Icon = "gear",
            //    Order = 4,
            //    Visible = true
            //}
        };

        return Task.FromResult<IEnumerable<NavigationItem>>(navigationItems);
    }
}
