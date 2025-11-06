using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GMGInventory.Data.OrderAppDB
{
    public class AppOrderDbContext : IdentityDbContext
    {
        public AppOrderDbContext(DbContextOptions<AppOrderDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.HasAnnotation("Relational:Schema", "dbo");
        }

        public DbSet<PAO_Orders> Orders { get; set; }
    }
}