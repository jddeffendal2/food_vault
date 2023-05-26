import { Request } from "@/requests/request";

export default class RecipeRequest extends Request {
  constructor() {
    super("Recipe");
  }

  createRecipe = async function(recipeInfo) {
    return await this.makeAuthenticatedRequest("Create", "POST", recipeInfo);
  }

  getUserRecipes = async function(userId) {
    return await this.makeAuthenticatedRequest(userId, "GET");
  }
}