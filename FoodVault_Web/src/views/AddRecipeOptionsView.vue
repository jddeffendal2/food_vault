<template>
  <div class="div-main">
    <h2>Create/Add Your Own Recipes To Food Vault</h2>
    <br />
    <label for="recipeName">Name of Recipe: </label>
    <input type="text" id="recipeName" v-model="recipeName" />
    <br /><br />
    <label for="ingredients"><b>Ingredients:</b></label>
    <div v-for="ingredient in ingredients" :key="ingredient">
      <span>{{ ingredient.id }}: </span>
      <input type="text" :v-model="ingredient.text">
    </div>
    <br />
    <label for="instructionOne"><b>Instructions: </b></label>
    <div v-for="instruction in instructions" :key="instruction">
      <span>{{ instruction.id }}: </span>
      <input type="text" :v-model="instruction.text">
    </div>
    <br /><br />
    <div id="extraInstructions"></div>
    <br /><br />
    <button id="addInstruction" @click="addInstruction">Add Another Instruction</button>
    <br /><br />
    <button id="saveRecipe" @click="saveRecipe">Save Recipe</button>
  </div>
</template>

<script setup>
import { ref } from "vue";

const ingredients = ref([]);
const instructions = ref([]);

const addIngredient = function() {
  var newIngredient = {
    id: ingredients.value.length,
    text: ""
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
</style>