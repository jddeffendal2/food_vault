namespace FoodVaultApi.Models
{
    public class RecipeInstruction
    {
        public string Id { get; set; }
        public string RecipeId { get; set; }
        public string Text { get; set; }
        public int SortOrder { get; set; }

        public Recipe Recipe { get; set; } // The recipe this instruction belongs to
    }
}
