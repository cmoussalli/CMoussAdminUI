using CMouss.ENVs;
using CMouss.IdentityFramework;
using CMouss.IdentityFramework.BlazorUI;
using CMoussAdminUI.Extensions;
using CMoussAdminUIDemoApp.Components;
using CMoussAdminUIDemoApp.Models;
using CMoussAdminUIDemoApp.Services;
using App = CMoussAdminUIDemoApp.Components.App;

namespace CMoussAdminUIDemoApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();



            builder.Services.AddIdentityFrameworkBlazorUI();

            // Add CMoussAdminUI services with navigation provider and theme customization
            builder.Services.AddCMoussAdminUI(options =>
            {
                options.NavigationProviderType = typeof(UserBasedNavigationProvider);
                options.UseNotificationDb = true; // Enable database-backed notification storage

                //Theme customization example(uncomment and modify as needed):
                 options.Theme.ApplicationName = "My App1";
                options.Theme.DefaultPageTitle = "My App 1";
                options.Theme.Topbar.Background = "linear-gradient(135deg, #667eea 0%, #764ba2 100%)";
                //options.Theme.Topbar.Background = "linear-gradient(135deg, #667eea 0%, #764ba2 100%)";
                options.Theme.Topbar.TextColor = "#ffffff";
                options.Theme.Sidebar.Background = "#171332";//2809fb //#0f172a
                options.Theme.Colors.Primary = "#3b82f6";
                //options.Theme.Logo.IconClass = "bi-building";
                options.Theme.Logo.ImageUrl = "/images/logo.png"; // Use image instead of icon
            });

            var app = builder.Build();

            // Register CMoussAdminUI middleware (auto-registers navigation provider)
            app.UseCMoussAdminUI();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);

            app.UseAntiforgery();

            app.MapStaticAssets();
            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            ENVManager.UseEnvironment("CMoussAdminUIDemoApp");
            string databaseTypeSTR = ENVManager.GetValue("DatabaseType");
            string connectionString = ENVManager.GetValue("ConnectionString");
            string tokenEncryptionKey = ENVManager.GetValue("TokenEncryptionKey");
            string administratorUserName = ENVManager.GetValue("AdministratorUserName");
            string administratorPassword = ENVManager.GetValue("AdministratorPassword");

            DatabaseType databaseType = DatabaseType.MSSQL;
            if (databaseTypeSTR.ToLower() == "sqlite")
            {
                databaseType = DatabaseType.SQLite;
            }

            IDFManager.Configure(new IDFManagerConfig
            {
                DatabaseType = databaseType,
                //DBConnectionString = "Server=projcetbravossql.database.windows.net;Database=ProjectBravos;User Id=BravosAdmin;Password=BPl@yer23212;",
                DBConnectionString = connectionString,
                DefaultListPageSize = 25,
                DBLifeCycle = DBLifeCycle.Both,
                IsActiveByDefault = true,
                IsLockedByDefault = false,
                DefaultTokenLifeTime = new LifeTime(365, 0, 0),
                AllowUserMultipleSessions = false,
                TokenEncryptionKey = tokenEncryptionKey,
                TokenValidationMode = TokenValidationMode.DecryptOnly,
                AdministratorUserName = administratorUserName,
                AdministratorPassword = administratorPassword,
            });

            DemoAppDBContext db = new();
            db.Database.EnsureCreated();
            db.InsertMasterData();
            IDFManager.RefreshIDFStorage();

            IDFBlazorUIConfig.AuthHomeURL = "/workspace";
            IDFBlazorUIConfig.AfterLogoutRedirectURL = "/";
            IDFBlazorUIConfig.LoginRedirectURL = "/login";




            app.Run();
        }
    }
}
