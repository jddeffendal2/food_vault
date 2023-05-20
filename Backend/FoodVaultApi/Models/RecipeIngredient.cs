namespace FoodVaultApi.Models
{
    public class RecipeIngredient
    {
        public string Id { get; set; }
        public string RecipeId { get; set; }
        public string Name { get; set; }
        public double Quantity { get; set; }
        public string UnitOfMeasurement { get; set; }
        public int SortOrder { get; set; }

        public Recipe Recipe { get; set; } // The recipe this ingredient belongs to
    }
}
