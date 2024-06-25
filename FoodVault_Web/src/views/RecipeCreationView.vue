<template>
  <div class="div-main">
    <h2 id="createRecipeTitle">Create/Add Your Own Recipes To Food Vault</h2>
    <br />
    <label>Name of Recipe: </label>
    <input type="text" name="recipeName" v-model="recipeName" />
    <br /><br />
    <label>Description of Recipe: </label>
    <input type="text" name="description" v-model="description" />
    <br /><br />
    <div class="recipeInformation">
      <div class="ingredientBox">
        <label id="ingredientLabel"><b>Ingredients:</b></label> <br /><br />
        <div class="ingredients" v-if="ingredients.length > 0">
          <br />
          <div class="ingredientInputs" v-for="ingredient in ingredients" :key="ingredient">
            <span>{{ ingredient.id + 1 }}: </span>
            <input type="text" :id="'ingredientName' + ingredient.id" placeholder="Name of Ingredient" v-model="ingredient.text" />
            <input class="amount" type="text" :id="'amount' + ingredient.id" placeholder="Amount" v-model="ingredient.amount" />
            <input class="termOfMeasurements" type="text" :id="'termOfMeasurement' + ingredient.id" placeholder="mg" v-model="ingredient.measurementTerm" />
            <br />
            <br />
          </div>
        </div>
        <br />
        <FvButton id="addIngredient" @click="addIngredient">
          Add Another Ingredient
        </FvButton>
        <br />
        <br />
      </div>
      <div class="instructionsBox">
        <label id="instructionsLabel"><b>Instructions: </b></label> <br /><br />
        <div class="instructions" v-if="instructions.length > 0">
          <br />
          <div v-for="instruction in instructions" :key="instruction.id">
            <span>{{ instruction.sortOrder }}: </span>
            <input type="text" v-model="instruction.text" />
            &nbsp;&nbsp;
            <button v-if="instructions.length > 1 && instruction.order !== 1" @click="shiftUp(instruction)">&#8593;</button>
            <button v-if="instructions.length > 1 && instruction.order !== instructions.length" @click="shiftDown(instruction)">&#8595;</button>
          </div>
        </div>
        <div id="extraInstructions"></div>
        <br /><br />
        <FvButton id="addInstruction" @click="addInstruction">
          Add Another Instruction
        </FvButton>
        <br /><br />
      </div>
    </div>
    <FvButton @click="createRecipe">Save Recipe</FvButton>

    <br /><br /><br />
  </div>
</template>

<script setup>
import { ref } from "vue";
import { useAccountStore } from "@/stores/accountStore";
import { useRouter } from "vue-router";
import { RecipeRequest } from "@/requests/recipe-request";
import { RecipeIngredientRequest } from "@/requests/recipe-ingredient-request";
import { RecipeInstructionRequest } from "@/requests/recipe-instruction-request";
import FvButton from "@/components/shared/FvButton.vue";

const router = useRouter();

const accountStore = useAccountStore();

const ingredients = ref([]);
const instructions = ref([]);

const recipeName = ref("");
const description = ref("");

const createRecipe = async function () {
  const recipe = {
    creator: accountStore.currentUserId,
    name: recipeName.value,
    description: description.value,
  };
  const recipeId = await new RecipeRequest().createRecipe(recipe);

  for (let i = 0; i < ingredients.value.length; i++) {
    var newIngredient = {
      recipeId: recipeId,
      name: ingredients.value[i].text,
      quantity: ingredients.value[i].amount,
      unitOfMeasurement: ingredients.value[i].measurementTerm,
      sortOrder: i + 1,
    };
    await new RecipeIngredientRequest().createRecipeIngredient(newIngredient);
  }

  for (let i = 0; i < instructions.value.length; i++) {
    var newInstruction = {
      recipeId: recipeId,
      text: instructions.value[i].text,
      sortOrder: i + 1,
    };
    await new RecipeInstructionRequest().createRecipeInstruction(newInstruction);
  } 
  router.push("/Recipes");
};

const addIngredient = function () {
  var newIngredient = {
    id: ingredients.value.length,
    text: "",
    amount: "",
    measurementTerm: "",
    sortOrder: ingredients.value.length + 1
  };
  ingredients.value.push(newIngredient);
};
const addInstruction = function () {
  var newInstruction = {
    id: instructions.value.length,
    text: "",
    sortOrder: instructions.value.length + 1
  };
  instructions.value.push(newInstruction);
};

const shiftUp = function (instruction) {
  const oldIndex = instructions.value.indexOf(instruction);
  instructions.value.splice(oldIndex, 1);
  instructions.value.splice(oldIndex - 1, 0, instruction);
  updateInstructionOrderLabels();
}

const shiftDown = function (instruction) {
  const oldIndex = instructions.value.indexOf(instruction);
  instructions.value.splice(oldIndex, 1);
  instructions.value.splice(oldIndex + 1, 0, instruction);
  updateInstructionOrderLabels();
}

const updateInstructionOrderLabels = function () {
  for (let i = 0; i < instructions.value.length; i++) {
    instructions.value[i].sortOrder = i + 1;
  }
}
</script>

<style scoped>
.div-main {
  text-align: center;
  color: #043565;
}

.recipeInformation {
  display: flex;
  justify-content: space-between;
  flex-direction: row;
  flex-wrap: wrap;
}

.recipeInformation>div {
  flex: 30%;
  margin: 20px;
}

.ingredients,
.instructions {
  border: 3px solid #c7d6d5;
  border-radius: 5px;
  background-color: #c7d6d5;
  justify-content: center;
}

.ingredientInputs input {
  margin: 1px;
}

.amount {
  min-width: 70px;
  max-width: 70px;
}

.termOfMeasurements {
  min-width: 20px;
  max-width: 20px;
}

#ingredientLabel {
  font-size: 20px;
  color: #043565;
}

#instructionsLabel {
  font-size: 20px;
  color: #043565;
}

.instructionHover {
  max-width: 300px;
  min-width: 300px;
  margin-left: auto;
  margin-right: auto;
  margin-bottom: 5px;
}

.instructionHover:hover {
  cursor: pointer;
}
</style>
