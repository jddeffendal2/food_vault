import { Request } from "@/requests/request";

export class RecipeRequest extends Request {
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

  isUserAllowedToViewRecipe = async function (userId, recipeId) {
    return await this.makeRequest(`User/${userId}/Recipe/${recipeId}/IsAuthorized`, "GET")
  }

  isUserOwnerOfRecipe = async function (recipeId, userId) {
    return await this.makeRequest(`Recipe/${recipeId}/User/${userId}/IsOwner`, "GET")
  }

  updateRecipeName = async function (recipeId, recipeNewName){
    return await this.makeRequest(`Recipe/${recipeId}/${recipeNewName}`, "PUT")
  }

  updateRecipeDescription = async function(recipeId, recipeNewDescription) {
    return await this.makeRequest(`Recipe/${recipeId}/Description/${recipeNewDescription}`, "PUT")
  }
}