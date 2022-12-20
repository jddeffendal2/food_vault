using Microsoft.EntityFrameworkCore;

namespace FoodVault.Api.Data
{
    public class FoodVaultDbContext : DbContext
    {
        public FoodVaultDbContext()
        {
        }

        public FoodVaultDbContext(DbContextOptions<FoodVaultDbContext> options) : base(options)
        {
        }

        // entities
        public DbSet<> Users { get; set; }
        public DbSet<> Groups { get; set; }
    }
}
