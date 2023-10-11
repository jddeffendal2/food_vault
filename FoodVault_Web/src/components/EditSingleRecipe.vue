<template>
  <h2>Edit This Recipe</h2>
  <h3>
    Name:
    <input
      type="text"
      class="recipeDescription"
      name="recipeName"
      v-model="recipeName"
    />
  </h3>
  <div>
    Description:
    <input
      type="text"
      class="recipeDescription"
      name="recipeName"
      v-model="recipeDescription"
    />
  </div>
  <div>
    <h3>Ingredients</h3>
    <div
      v-for="ingredient in updatedIngredients"
      :key="ingredient.ingredientId"
    >
      <input type="text" v-model="ingredient.name" /> -
      <input
        type="text"
        class="ingredientQuantity"
        v-model="ingredient.quantity"
      />
      &nbsp;<input
        type="text"
        class="ingredientsMeasurementUnit"
        v-model="ingredient.unitOfMeasurement"
      />
      <button
        class="recipeDeleteBtn"
        @click="deleteIngredient(ingredient.ingredientId)"
      >
        X
      </button>
    </div>
    <br />
    <button @click="addNewIngredient">Add Ingredient</button>
  </div>
  <div>
    <h3>Instructions</h3>
    <div v-for="instruction in updatedInstructions " :key="instruction.id">
      {{ instruction.sortOrder }}. <input class="instructionText" v-model="instruction.text"/> 
      <button
        class="recipeDeleteBtn"
        @click="deleteInstruction(instruction.id)"
      >
        X
      </button>
    </div>
    <br />
    <button @click="addNewInstruction">Add Instruction</button>
  </div>
  <br /><br />
  <FvButton @click="updateRecipe">Update Recipe</FvButton>
</template>

<script setup>
import { onMounted, ref } from "vue";
import { useRouter } from "vue-router";
import { RecipeRequest } from "@/requests/recipe-request";
import { RecipeIngredientRequest } from "@/requests/recipe-ingredient-request";
import { RecipeInstructionRequest } from "@/requests/recipe-instruction-request";
import FvButton from "@/components/shared/FvButton.vue";

const router = useRouter();

const recipeRequest = new RecipeRequest();
const recipeIngredientRequest = new RecipeIngredientRequest();
const recipeInstructionRequest = new RecipeInstructionRequest();
const recipe = ref({});
const ingredients = ref([]);
const instructions = ref([]);
const recipeDescription = ref("");
const recipeName = ref("");
const updatedIngredients = ref([]);
const updatedInstructions = ref([]);


const props = defineProps({
  recipeId: {
    type: String,
    required: true,
  },
});

onMounted(async () => {
  recipe.value = await recipeRequest.getRecipeById(props.recipeId);
  recipeName.value = recipe.value.name;
  recipeDescription.value = recipe.value.description;
  ingredients.value = await recipeIngredientRequest.getAllIngredientsForRecipe(
    props.recipeId
  );
  for (let i = 0; i < ingredients.value.length; i++) {
    updatedIngredients.value.push({
      ingredientId: ingredients.value[i].ingredientId,
      recipeId: ingredients.value[i].recipeId,
      name: ingredients.value[i].name,
      quantity: ingredients.value[i].quantity,
      unitOfMeasurement: ingredients.value[i].unitOfMeasurement,
      sortOrder: ingredients.value[i].sortOrder,
    });
  }
  instructions.value =
    await recipeInstructionRequest.getAllInstructionsForRecipe(props.recipeId);
  for (let i = 0; i < instructions.value.length; i++) {
    updatedInstructions.value.push({
      id: instructions.value[i].id,
      recipeId: instructions.value[i].recipeId,
      text: instructions.value[i].text,
      sortOrder: instructions.value[i].sortOrder,
    });
  }
});

const addNewIngredient = function () {
  var newIngredient = {
    ingredientId: 0,
    recipeId: props.recipeId,
    name: "",
    quantity: "",
    unitOfMeasurement: "",
    sortOrder: updatedIngredients.value.length + 1,
  };
  updatedIngredients.value.push(newIngredient);
};
const addNewInstruction = function () {
  var newInstruction = {
    id: 0,
    recipeId: props.recipeId,
    text: "",
    sortOrder: updatedInstructions.value.length + 1
  }
  updatedInstructions.value.push(newInstruction)
}

const deleteIngredient = async function (ingredientId) {
  var ingredient = updatedIngredients.value.find(x => x.ingredientId === ingredientId);
  var index = updatedIngredients.value.indexOf(ingredient);
  
  var doesIngredientExistInDb = ref(false);
  for (let i=0; i<ingredients.value.length; i++) {
    if (ingredients.value[i].ingredientId == ingredientId) {
      doesIngredientExistInDb.value = true;
    }
  }
  if (doesIngredientExistInDb.value) {
    await recipeIngredientRequest.deleteRecipeIngredient(ingredientId);
  }
  updatedIngredients.value.splice(index, 1);
};

const deleteInstruction = async function (instructionId) {
  var instruction = updatedInstructions.value.find(x => x.id === instructionId);
  var index = updatedInstructions.value.indexOf(instruction);

  var doesInstructionExistInDb = ref(false);
  for (let i=0; i<instructions.value.length; i++) {
    if (instructions.value[i].id == instructionId) {
      doesInstructionExistInDb.value = true;
    }
  } 
  if (doesInstructionExistInDb.value) {
    await recipeInstructionRequest.deleteRecipeInstruction(instructionId);
  }
  updatedInstructions.value.splice(index, 1);
};

const cleanSortOrders = function () {
  for (let i = 0; i < updatedIngredients.value.length; i++) {
    updatedIngredients.value[i].sortOrder = i + 1;
  }
  for (let i=0; i<updatedInstructions.value.length; i++) {
    updatedInstructions.value[i].sortOrder = i + 1;
  }
}

const updateRecipe = async function () {
  cleanSortOrders();
  if (recipeName.value != recipe.value.name) {
    await recipeRequest.updateRecipeName(props.recipeId, recipeName.value);
  }
  if (recipeDescription.value != recipe.value.description) {
    await recipeRequest.updateRecipeDescription(
      props.recipeId,
      recipeDescription.value
    );
  }
  const areTheIngredientArraysEqual = ref(true);
  for (let i = 0; i < updatedIngredients.value.length; i++) {
    if (
      updatedIngredients.value.length != ingredients.value.length ||
      updatedIngredients.value[i].name != ingredients.value[i].name ||
      updatedIngredients.value[i].quantity != ingredients.value[i].quantity ||
      updatedIngredients.value[i].unitOfMeasurement !=
        ingredients.value[i].unitOfMeasurement ||
      updatedIngredients.value[i].sortOrder != ingredients.value[i].sortOrder
    ) {
      areTheIngredientArraysEqual.value = false;
    }
  }
  if (areTheIngredientArraysEqual.value == false) {
    for (let i = 0; i < updatedIngredients.value.length; i++) {
      if (updatedIngredients.value[i].ingredientId == 0) {
        await recipeIngredientRequest.createRecipeIngredient({
          recipeId: updatedIngredients.value[i].recipeId,
          name: updatedIngredients.value[i].name,
          quantity: updatedIngredients.value[i].quantity,
          unitOfMeasurement: updatedIngredients.value[i].unitOfMeasurement,
          sortOrder: updatedIngredients.value[i].sortOrder,
        });
      } else {
        await recipeIngredientRequest.updateRecipeIngredients(
          props.recipeId,
          updatedIngredients.value[i].ingredientId,
          updatedIngredients.value[i]
        );
      }
    }
  }
  const areTheInstructionArraysEqual = ref(true);
  for (let i = 0; i < updatedInstructions.value.length; i++) {
    if (
      updatedInstructions.value.length != instructions.value.length ||
      updatedInstructions.value[i].text != instructions.value[i].text ||
      updatedInstructions.value[i].sortOrder != instructions.value[i].sortOrder
    ) {
      areTheInstructionArraysEqual.value = false;
    }
  }
  if (areTheInstructionArraysEqual.value == false) {
    for (let i = 0; i < updatedInstructions.value.length; i++) {
      if (updatedInstructions.value[i].id == 0) {
        await recipeInstructionRequest.createRecipeInstruction({
          recipeId: updatedInstructions.value[i].recipeId,
          text: updatedInstructions.value[i].text,
          sortOrder: updatedInstructions.value[i].sortOrder,
        });
      } else {
        await recipeInstructionRequest.updateRecipeInstructions(
          props.recipeId,
          updatedInstructions.value[i].id,
          updatedInstructions.value[i]
        );
      }
    }
  }
  router.push("/Recipe/" + props.recipeId);
};
</script>

<style scoped>
.recipeDescription {
  width: 400px;
  height: 25px;
  white-space: initial;
}

.ingredientQuantity {
  width: 30px;
}

.ingredientsMeasurementUnit {
  width: 60px;
}

.instructionText {
  width: 350px;
}
.recipeDeleteBtn {
  border: none;
  cursor: pointer;
  font-weight: bold;
  color: #043565;
  background: transparent;
}
</style>
