namespace FoodVaultApi.Models
{
    public class GroupRecipe
    {
        public string Id { get; set; }
        public string GroupId { get; set; }
        public string RecipeId { get; set; }

        public Group Group { get; set; } // The group for this grouprecipe entry
        public Recipe Recipe { get; set; } // The recipe for this grouprecipe entry
    }
}
