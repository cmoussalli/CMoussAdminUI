using CMouss.IdentityFramework;
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

        string? tok = null;
        if (_httpContextAccessor.HttpContext?.Request.Cookies
                .TryGetValue("IDF_AuthToken", out tok) == true
            && !string.IsNullOrWhiteSpace(tok))
        {
            CMouss.IdentityFramework.AuthService authService = new CMouss.IdentityFramework.AuthService();
            AuthResult res = authService.AuthUserToken(tok, TokenValidationMode.DecryptOnly);

            UserService userService = new UserService();
            List<Role> roles = userService.GetRoles(res.UserToken.User.Id);
            List<EntityAllowedActionsModel> permissions = userService.GetUserPermissions(res.UserToken.User.Id);

            items.Add(new NavigationItem
            {
                Id = "authenticated",
                Text = "Authenticated",
                Url = "/authenticated",
                Icon = "house-door",
                Order = 1000,
                Visible = true
            });

            if(roles.Exists(o => o.Id.ToLower() == "administrators"))
            {
                items.Add(new NavigationItem
                {
                    Id = "adminPageRole",
                    Text = "Admin Role",
                    Url = "/adminPageRole",
                    Icon = "house-door",
                    Order = 1001,
                    Visible = true
                });
            }

            if (permissions.Exists(o => o.Entity.Id.ToLower() == "user" && o.AllowedActions.Any( a => a.Id == "search")))
            {
                
                items.Add(new NavigationItem
                {
                    Id = "adminPagePermission",
                    Text = "Admin Permission",
                    Url = "/adminPagePermission",
                    Icon = "house-door",
                    Order = 1002,
                    Visible = true
                });


            }


        }





        return Task.FromResult<IEnumerable<NavigationItem>>(items);
    }
}
