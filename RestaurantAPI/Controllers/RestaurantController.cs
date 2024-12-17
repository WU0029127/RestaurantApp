using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Services;
using RestaurantApp.Restaurant; 

namespace RestaurantAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;

        public RestaurantController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        // GET: api/restaurant
        [HttpGet]
        public async Task<IActionResult> GetRestaurants()
        {
            var restaurants = await _restaurantService.GetRestaurantsAsync();
            return Ok(restaurants);
        }

        // GET: api/restaurant/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRestaurantById(int id)
        {
            var restaurant = await _restaurantService.GetRestaurantByIdAsync(id);
            if (restaurant == null) return NotFound();
            return Ok(restaurant);
        }

        // POST: api/restaurant
        [Authorize] 
        [HttpPost]
        public async Task<IActionResult> AddRestaurant([FromBody] RestaurantApp.Restaurant.Restaurant restaurant)  // Using RestaurantApp.Restaurant.Restaurant
        {
            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }

            var newRestaurant = await _restaurantService.AddRestaurantAsync(restaurant);
            return CreatedAtAction(nameof(GetRestaurantById), new { id = newRestaurant.Id }, newRestaurant);
        }

        // PUT: api/restaurant/{id}
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRestaurant(int id, [FromBody] RestaurantApp.Restaurant.Restaurant updatedRestaurant) 
        {
            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }

            var success = await _restaurantService.UpdateRestaurantAsync(id, updatedRestaurant);
            if (!success) return NotFound();
            return NoContent();
        }

        // DELETE: api/restaurant/{id}
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRestaurant(int id)
        {
            var success = await _restaurantService.DeleteRestaurantAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }

        // POST: api/restaurant/{id}/reviews
        [Authorize]
        [HttpPost("{id}/reviews")]
        public async Task<IActionResult> AddReview(int id, [FromBody] RestaurantApp.Review.Review review)
        {
            var restaurant = await _restaurantService.AddReviewAsync(id, review);
            if (restaurant == null) return NotFound();
            return CreatedAtAction(nameof(GetRestaurantById), new { id = restaurant.Id }, restaurant);
        }
    }
}