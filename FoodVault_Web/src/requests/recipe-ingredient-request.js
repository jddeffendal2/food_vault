import { Request } from "@/requests/request";

export class RecipeIngredientRequest extends Request {
  constructor() {
    super("RecipeIngredient");
  }

  createRecipeIngredient = async function(ingredientInfo) {
    return await this.makeRequest("Create", "POST", ingredientInfo);
  }

  getAllIngredientsForRecipe = async function(recipeId) {
    return await this.makeRequest(`Recipe/${recipeId}`, "GET");
  }
}