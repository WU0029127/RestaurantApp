namespace RestaurantApp.Review
{
    public class Review
    {
        public int Id { get; set; }
        public int RestaurantId { get; set; }
        public string? ReviewerName { get; set; }
        public string? Comments { get; set; }
        public int Rating { get; set; }
    }
}