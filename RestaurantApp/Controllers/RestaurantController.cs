using Microsoft.AspNetCore.Mvc;
using RestaurantApp.Restaurant;

namespace RestaurantApp.Controllers
{
    public class RestaurantController : Controller
    {
        private readonly ILogger<RestaurantController> _logger;

        public RestaurantController(ILogger<RestaurantController> logger)
        {
            _logger = logger;
        }

        // In-Memory List of Restaurants for Simplicity
        private static readonly List<RestaurantApp.Restaurant.Restaurant> _restaurants = new List<RestaurantApp.Restaurant.Restaurant>
        {
            new RestaurantApp.Restaurant.Restaurant { Id = 1, Name = "Mastro's", Location = "Costa Mesa", CuisineType = "American", Rating = 4.5 },
            new RestaurantApp.Restaurant.Restaurant { Id = 2, Name = "Nobu", Location = "Newport Beach", CuisineType = "Japanese", Rating = 4.2 }
        };

        // GET: Restaurant
        public IActionResult Index()
        {
            return View(_restaurants);
        }

        // GET: Restaurant/Details
        public IActionResult Details(int id)
        {
            var restaurant = _restaurants.FirstOrDefault(r => r.Id == id);
            if (restaurant == null)
            {
                return NotFound();
            }
            return View(restaurant);
        }

        // GET: Restaurant/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Restaurant/Create
        [HttpPost]
        public IActionResult Create(RestaurantApp.Restaurant.Restaurant restaurant)
        {
            if (ModelState.IsValid)
            {
                restaurant.Id = _restaurants.Max(r => r.Id) + 1;
                _restaurants.Add(restaurant);
                return RedirectToAction(nameof(Index));
            }
            return View(restaurant);
        }

        // GET: Restaurant/Edit
        public IActionResult Edit(int id)
        {
            var restaurant = _restaurants.FirstOrDefault(r => r.Id == id);
            if (restaurant == null)
            {
                return NotFound();
            }
            return View(restaurant);
        }

        // POST: Restaurant/Edit
        [HttpPost]
        public IActionResult Edit(int id, RestaurantApp.Restaurant.Restaurant updatedRestaurant)
        {
            var restaurant = _restaurants.FirstOrDefault(r => r.Id == id);
            if (restaurant == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                restaurant.Name = updatedRestaurant.Name;
                restaurant.Location = updatedRestaurant.Location;
                restaurant.CuisineType = updatedRestaurant.CuisineType;
                restaurant.Rating = updatedRestaurant.Rating;
                return RedirectToAction(nameof(Index));
            }
            return View(updatedRestaurant);
        }

        // GET: Restaurant/Delete
        public IActionResult Delete(int id)
        {
            var restaurant = _restaurants.FirstOrDefault(r => r.Id == id);
            if (restaurant == null)
            {
                return NotFound();
            }
            return View(restaurant);
        }

        // POST: Restaurant/Delete
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var restaurant = _restaurants.FirstOrDefault(r => r.Id == id);
            if (restaurant != null)
            {
                _restaurants.Remove(restaurant);
            }
            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error");
        }
    }
}