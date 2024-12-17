using System.ComponentModel.DataAnnotations;

namespace RestaurantApp.Restaurant
{
    public class Restaurant
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name can't be longer than 100 characters.")]
        public string Name { get; set; } = "";

        [Required(ErrorMessage = "Location is required.")]
        [StringLength(200, ErrorMessage = "Location can't be longer than 200 characters.")]
        public string Location { get; set; } = "";

        [Required(ErrorMessage = "Cuisine Type is required.")]
        [StringLength(50, ErrorMessage = "Cuisine Type can't be longer than 50 characters.")]
        public string CuisineType { get; set; } = "";

        [Range(0, 5, ErrorMessage = "Rating must be between 0 and 5.")]
        public double Rating { get; set; }

        public List<RestaurantApp.Review.Review> Reviews { get; set; } = new();
    }
}