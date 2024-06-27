namespace FoodVaultApi.Models

{
    public class RecipeView
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string RecipeId { get; set; }

        public User User { get; set; } 
        public Recipe Recipe { get; set; } 
    }
}