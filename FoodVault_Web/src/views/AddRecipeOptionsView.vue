<template>
  <div class="div-main">
    <h2>Create/Add Your Own Recipes To Food Vault</h2>
    <br />
    <label>Name of Recipe: </label>
    <input type="text" name="recipeName" v-model="recipeName" />
    <br /><br />
    <label>Description of Recipe: </label>
    <input type="text" name="description" v-model="description" />
    <br/><br/>
    <div class="recipeInformation">
      <div class="ingredientBox">
        <label id="ingredientLabel"><b>Ingredients:</b></label> <br/><br/>
        <div class="ingredients" v-if="ingredients.length > 0">
          <br/>
          <div class="ingredientInputs" v-for="ingredient in ingredients" :key="ingredient">
            <span>{{ ingredient.id + 1}}: </span>
            <input type="text" :id="'ingredientName' + ingredient.id" placeholder="Name of Ingredient"/> 
            <input class="amount" type="number" :id="'amount' + ingredient.id" placeholder="Amount"/>
            <input class="termOfMeasurements" type="text" :id="'termOfMeasurement' + ingredient.id" placeholder="mg"/>
            <br/> <br/>
          </div>
        </div>
        <br/>
        <button id="addIngredient" @click="addIngredient">Add Another Ingredient</button>
        <br /> <br/>
      </div>
      <div class="instructionsBox">
        <label id="instructionsLabel"><b>Instructions: </b></label> <br/><br/>
        <div class="instructions" v-if="instructions.length > 0">
          <br/>
          <div v-for="instruction in instructions" :key="instruction">
            <span>{{ instruction.id + 1}}: </span>
            <input type="text" :id="'instructionText' + instruction.id" :placeholder="'Instruction #' + (instruction.id + 1)">
            <br/> <br/>
          </div>
        </div>
        <div id="extraInstructions"></div>
        <br /><br />
        <button id="addInstruction" @click="addInstruction">Add Another Instruction</button>
        <br /><br />
      </div>
    </div>
    <button id="saveRecipe" @click="createRecipe">Save Recipe</button>
  </div>
</template>

<script setup>
import { ref } from "vue";
import { useAccountStore } from "../stores/accountStore";
import { saveRecipe } from "../utils/request.js";
import { saveRecipeIngredient } from "../utils/request.js";
import { saveRecipeInstruction } from "../utils/request.js";

const accountStore = useAccountStore();

const ingredients = ref([]);
const instructions = ref([]);


var recipeName = ref("");
var description = ref("");

const createRecipe = async function() {
  const recipe = {
    creator: accountStore.currentUserId,
    name: recipeName.value, 
    description: description.value
  };
  let savedRecipe = await saveRecipe(recipe, (res) => res);
  
  for (let i=0; i<ingredients.value.length; i++) {
    const ingredientName = document.getElementById('ingredientName' + i).value;
    const amount = document.getElementById('amount' + i).value;
    const termOfMeasurement = document.getElementById('termOfMeasurement' + i).value;
    var newIngredient = {
      recipeId: savedRecipe,
      name: ingredientName,
      quantity: amount, 
      unitOfMeasurement: termOfMeasurement,
      sortOrder: i + 1
    };
    saveRecipeIngredient(newIngredient, (res) => console.log(res))
  }

  for (let i=0; i<instructions.value.length; i++) {
    const instructionText = document.getElementById('instructionText' + i).value;
    var newInstruction = {
      recipeId: savedRecipe,
      text: instructionText,
      sortOrder: i + 1
    };
    saveRecipeInstruction(newInstruction, (res) => console.log(res))
  }
}

const addIngredient = function() {
  var newIngredient = {
    id: ingredients.value.length,
    text: "",
    amount: "", 
    measurementTerm: ""
  };
  ingredients.value.push(newIngredient);
}
const addInstruction = function() {
  var newInstruction = {
    id: instructions.value.length,
    text: ""
  };
  instructions.value.push(newInstruction);
}

</script>

<style scoped>
.div-main {
  text-align: center;
}

.recipeInformation {
  display: flex;
  justify-content: space-between;
  flex-direction: row;
  flex-wrap: wrap;
}

.recipeInformation > div {
  flex: 30%;
  margin: 20px;
}

.ingredients,
.instructions {
  border: 3px solid #C7D6D5;
  border-radius: 5px;
  background-color: #C7D6D5;
}

.ingredientInputs input{
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
</style>