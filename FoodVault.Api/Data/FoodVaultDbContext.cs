using Microsoft.EntityFrameworkCore;

namespace FoodVault.Api.Data
{
    public class FoodVaultDbContext : DbContext
    {
        public FoodVaultDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        // entities
        public DbSet<> Users { get; set; }
        public DbSet<> Groups { get; set; }
    }
}
