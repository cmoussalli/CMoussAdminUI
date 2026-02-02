using CMoussAdminUI.Models;
using CMoussAdminUI.Services;

namespace CMoussAdminUIDemoApp.Services;

public class UserBasedNavigationProvider : INavigationProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public int Priority => 10;

    public UserBasedNavigationProvider(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Task<IEnumerable<NavigationItem>> GetNavigationItemsAsync()
    {
        var items = new List<NavigationItem>
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

        if (_httpContextAccessor.HttpContext?.Request.Cookies
                .TryGetValue("IDF_AuthToken", out var token) == true
            && !string.IsNullOrWhiteSpace(token))
        {
            items.Add(new NavigationItem
            {
                Id = "authenticated",
                Text = "Authenticated",
                Url = "/authenticated",
                Icon = "house-door",
                Order = 3,
                Visible = true
            });
        }

        return Task.FromResult<IEnumerable<NavigationItem>>(items);
    }
}
