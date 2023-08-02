import { Request } from "@/requests/request";

export default class GroupRecipeRequest extends Request {
  constructor() {
    super("GroupRecipe");
  }

  createGroupRecipe = async function(groupRecipeInfo) {
    return await this.makeRequest("Create", "POST", groupRecipeInfo);
  }

  getRecipesInGroup = async function(groupId) {
    return await this.makeRequest(groupId, "GET");
  }
  getGroupsThatContainRecipe = async function(recipeId) {
    return await this.makeRequest("GetGroupsThatContainRecipe/" + recipeId, "GET");
  }
}