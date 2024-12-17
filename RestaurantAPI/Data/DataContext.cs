using Microsoft.EntityFrameworkCore;
using RestaurantApp;

namespace RestaurantApp.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        // DbSet for Restaurants
        public DbSet<RestaurantApp.Restaurant.Restaurant> Restaurants { get; set; } = null!;
        
        // DbSet for Reviews (add this)
        public DbSet<RestaurantApp.Review.Review> Reviews { get; set; } = null!;
    }
}