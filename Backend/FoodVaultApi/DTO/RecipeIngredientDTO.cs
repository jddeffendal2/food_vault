using FoodVaultApi.Models;

namespace FoodVaultApi.DTO
{
    public class RecipeIngredientDTO
    {
        public string name { get; set; }
        public string recipeId { get; set; }
        public double quantity { get; set; }
        public string unitOfMeasurement { get; set; }
        public int sortOrder { get; set; }
    }

    public class RecipeIngredientGetDTO
    {
        public string ingredientId { get; set; }
        public string recipeId { get; set; }
        public string name { get; set; }
        public double quantity { get; set; }
        public string unitOfMeasurement { get; set; }
        public int sortOrder { get; set; }

        public static RecipeIngredientGetDTO ToDTO(RecipeIngredient ingredient)
        {
            return new RecipeIngredientGetDTO
            {
                ingredientId = ingredient.Id,
                recipeId = ingredient.RecipeId,
                name = ingredient.Name,
                quantity = ingredient.Quantity,
                unitOfMeasurement = ingredient.UnitOfMeasurement,
                sortOrder = ingredient.SortOrder
            };
        }
    }
}
