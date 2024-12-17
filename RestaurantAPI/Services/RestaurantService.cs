using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RestaurantApp.Data;
using RestaurantApp.Restaurant;

namespace RestaurantAPI.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly DataContext _context;

        public RestaurantService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<RestaurantApp.Restaurant.Restaurant>> GetRestaurantsAsync()
        {
            return await _context.Restaurants.ToListAsync();
        }

        public async Task<RestaurantApp.Restaurant.Restaurant?> GetRestaurantByIdAsync(int id)
        {
            return await _context.Restaurants.Include(r => r.Reviews).FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<RestaurantApp.Restaurant.Restaurant> AddRestaurantAsync(RestaurantApp.Restaurant.Restaurant restaurant)
        {
            _context.Restaurants.Add(restaurant);
            await _context.SaveChangesAsync();
            return restaurant;
        }

        public async Task<bool> UpdateRestaurantAsync(int id, RestaurantApp.Restaurant.Restaurant updatedRestaurant)
        {
            var restaurant = await _context.Restaurants.FindAsync(id);
            if (restaurant == null) return false;

            restaurant.Name = updatedRestaurant.Name;
            restaurant.Location = updatedRestaurant.Location;
            restaurant.CuisineType = updatedRestaurant.CuisineType;
            restaurant.Rating = updatedRestaurant.Rating;

            _context.Restaurants.Update(restaurant);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteRestaurantAsync(int id)
        {
            // Fetch the restaurant along with its reviews
            var restaurant = await _context.Restaurants
                                           .Include(r => r.Reviews) // Ensure related reviews are loaded
                                           .FirstOrDefaultAsync(r => r.Id == id);

            if (restaurant == null) return false;

            // Delete associated reviews
            _context.Reviews.RemoveRange(restaurant.Reviews);

            // Delete the restaurant
            _context.Restaurants.Remove(restaurant);

            // Save changes to the database
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<RestaurantApp.Restaurant.Restaurant?> AddReviewAsync(int restaurantId, RestaurantApp.Review.Review review)
        {
            // Fetch the restaurant with the given ID
            var restaurant = await _context.Restaurants.Include(r => r.Reviews)
                .FirstOrDefaultAsync(r => r.Id == restaurantId);

            if (restaurant == null) return null;

            // Add the review to the restaurant's reviews
            review.RestaurantId = restaurantId;
            _context.Reviews.Add(review);

            // Save changes to the database
            await _context.SaveChangesAsync();

            return restaurant;
        }
    }
}