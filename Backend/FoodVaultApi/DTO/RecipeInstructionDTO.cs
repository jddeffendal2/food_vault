using FoodVaultApi.Models;

namespace FoodVaultApi.DTO
{
    public class RecipeInstructionDTO
    {
        public string id { get; set; } = string.Empty;
        public string recipeId { get; set; } = string.Empty;
        public string text { get; set; } = string.Empty;
        public int sortOrder { get; set; }

        public static RecipeInstructionDTO ToDTO(RecipeInstruction ri)
        {
            return new RecipeInstructionDTO
            {
                id = ri.Id,
                recipeId = ri.RecipeId,
                text = ri.Text,
                sortOrder = ri.SortOrder
            };
        }
    }
}
