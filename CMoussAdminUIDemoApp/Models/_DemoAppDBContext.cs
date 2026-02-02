using CMouss.IdentityFramework;
using CMoussAdminUI.Data;
using Microsoft.EntityFrameworkCore;

namespace CMoussAdminUIDemoApp.Models
{
    public class DemoAppDBContext:CMoussAdminUIDbContext
    {

        public DbSet<Order> Orders { get; set; }






    }



}
