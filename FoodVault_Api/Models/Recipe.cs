namespace FoodVault_Api.Models
{
    public class Recipe
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public User User { get; set; } // Creator of recipe
        public ICollection<GroupRecipe> GroupRecipes { get; set; } // Every group the recipe is shared with
        public ICollection<RecipeInstruction> Instructions { get; set; } // Every instruction for this recipe
        public ICollection<RecipeIngredient> Ingredients { get; set; } // Every ingredient in this recipe
    }
}
