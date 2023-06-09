import { Request } from "@/requests/request";

export default class RecipeRequest extends Request {
  constructor() {
    super("Recipe");
  }

  createRecipe = async function(recipeInfo) {
    return await this.makeRequest("Create", "POST", recipeInfo);
  }

  getUserRecipes = async function(userId) {
    return await this.makeRequest(userId, "GET");
  }

  getRecipeById = async function (recipeId) {
    return await this.makeRequest("GetRecipe/" + recipeId, "GET")
  }
}