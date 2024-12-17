using System.Collections.Generic;
using System.Threading.Tasks;
using RestaurantApp.Restaurant;

namespace RestaurantAPI.Services
{
    public interface IRestaurantService
    {
        Task<List<RestaurantApp.Restaurant.Restaurant>> GetRestaurantsAsync();
        Task<RestaurantApp.Restaurant.Restaurant?> GetRestaurantByIdAsync(int id);
        Task<RestaurantApp.Restaurant.Restaurant> AddRestaurantAsync(RestaurantApp.Restaurant.Restaurant restaurant);
        Task<bool> UpdateRestaurantAsync(int id, RestaurantApp.Restaurant.Restaurant updatedRestaurant);
        Task<bool> DeleteRestaurantAsync(int id);
        Task<RestaurantApp.Restaurant.Restaurant?> AddReviewAsync(int restaurantId, RestaurantApp.Review.Review review);
    }
}