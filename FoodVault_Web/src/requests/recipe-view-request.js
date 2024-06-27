import { Request } from "@/requests/request";

export class RecipeViewRequest extends Request {
    constructor() {
        super("RecipeView");
    }

    createRecipeView = async function(userId, recipeId) {
        return await this.makeRequest("Create", "POST", { userId, recipeId });
    }


}