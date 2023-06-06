import { Request } from "@/requests/request";

export default class GroupRecipeRequest extends Request {
  constructor() {
    super("GroupRecipe");
  }

  createGroupRecipe = async function(groupRecipeInfo) {
    return await this.makeAuthenticatedRequest("Create", "POST", groupRecipeInfo);
  }

  getRecipesInGroup = async function(groupId) {
    return await this.makeAuthenticatedRequest(groupId, "GET");
  }
}