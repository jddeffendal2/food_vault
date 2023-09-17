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
        class="deleteIngredientBtn"
        @click="deleteIngredient(ingredient.ingredientId)"
      >
        X
      </button>
    </div>
    <br />
    <button @click="addNewIngredient">Add Another Ingredient</button>
  </div>
  <div>
    <h3>Instructions</h3>
    <div v-for="instruction in instructions" :key="instruction.id">
      {{ instruction.sortOrder }}. {{ instruction.text }}
    </div>
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

const deleteIngredient = async function (ingredientId) {
  var ingredient = updatedIngredients.value.find(x => x.ingredientId === ingredientId);
  var index = updatedIngredients.value.indexOf(ingredient);
  
  await recipeIngredientRequest.deleteRecipeIngredient(ingredientId);
  updatedIngredients.value.splice(index, 1);
};

const cleanSortOrders = function () {
  for (let i = 0; i < updatedIngredients.value.length; i++) {
    updatedIngredients.value[i].sortOrder = i + 1;
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
  const areTheyEqual = ref(true);
  for (let i = 0; i < updatedIngredients.value.length; i++) {
    if (
      updatedIngredients.value.length != ingredients.value.length ||
      updatedIngredients.value[i].name != ingredients.value[i].name ||
      updatedIngredients.value[i].quantity != ingredients.value[i].quantity ||
      updatedIngredients.value[i].unitOfMeasurement !=
        ingredients.value[i].unitOfMeasurement ||
      updatedIngredients.value[i].sortOrder != ingredients.value[i].sortOrder
    ) {
      areTheyEqual.value = false;
    }
  }
  if (areTheyEqual.value == false) {
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

.deleteIngredientBtn {
  border: none;
  cursor: pointer;
  font-weight: bold;
  color: #043565;
  background: transparent;
}
</style>
