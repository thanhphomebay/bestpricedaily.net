using Microsoft.EntityFrameworkCore;
namespace bestpricedaily.Models
{
    public class BestPriceDailyDbContext : DbContext
    {
        public DbSet<Item> Items { get; set; }
        public DbSet<Order> Orders { get; set; }

        public BestPriceDailyDbContext(DbContextOptions<BestPriceDailyDbContext> options) : base(options) { }
    }
}