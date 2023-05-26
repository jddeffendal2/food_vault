import { Request } from "@/requests/request";

export default class RecipeInstructionRequest extends Request {
  constructor() {
    super("RecipeInstruction");
  }

  createRecipeInstruction = async function(instructionInfo) {
    return await this.makeAuthenticatedRequest("Create", "POST", instructionInfo);
  }
}