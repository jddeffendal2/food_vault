import { Request } from "@/requests/request";

export default class RecipeIngredientRequest extends Request {
  constructor() {
    super("RecipeIngredient");
  }

  createRecipeIngredient = async function(ingredientInfo) {
    return await this.makeAuthenticatedRequest("Create", "POST", ingredientInfo);
  }
}