using FoodVaultApi.Models;

namespace FoodVaultApi.DTO
{
    public class RecipeIngredientDTO
    {
        public string name { get; set; }
        public double quantity { get; set; }
        public string unitOfMeasurement { get; set; }
        public int sortOrder { get; set; }
    }
}
