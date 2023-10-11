import { Request } from "@/requests/request";

export class RecipeInstructionRequest extends Request {
  constructor() {
    super("RecipeInstruction");
  }

  createRecipeInstruction = async function(instructionInfo) {
    return await this.makeRequest("Create", "POST", instructionInfo);
  }

  getAllInstructionsForRecipe = async function(recipeId) {
    return await this.makeRequest(`Recipe/${recipeId}`, "GET")
  }

  updateRecipeInstructions = async function(recipeId, instructionId, updatedInstruction) {
    return await this.makeRequest(`RecipeInstructions/${recipeId}/InstructionId/${instructionId}`, "PUT", updatedInstruction)
  }

  deleteRecipeInstruction = async function(instructionId) {
    return await this.makeRequest(`RecipeInstructions/${instructionId}`, "DELETE")
  }
}