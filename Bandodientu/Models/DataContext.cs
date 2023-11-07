using Bandodientu.Models;
using Bandodientu.Areas.Admin.Models;
using Microsoft.EntityFrameworkCore;

namespace Bandodientu.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<User> User { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<AdminMenu> AdminMenus { get; set; }

    }
}
