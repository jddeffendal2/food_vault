<template>
  <h2>Edit This Recipe</h2>
  <h3>Name: <input type="text" class="recipeDescription" name="recipeName" v-model="recipeName" /></h3>
  <div>Description: <input type="text" class="recipeDescription" name="recipeName" v-model="recipeDescription" /></div>
  <div>
    <h3>Ingredients</h3>
    <div v-for="ingredient in ingredients" :key="ingredient.ingredientId">
      {{ ingredient.name }} - {{ ingredient.quantity }} {{ ingredient.unitOfMeasurement }}
    </div>
  </div>
  <div>
    <h3>Instructions</h3>
    <div v-for="instruction in instructions" :key="instruction.id">
      {{ instruction.sortOrder }}. {{ instruction.text }}
    </div>
  </div>
  <br/><br/>
  <FvButton @click="updateRecipe">Update Recipe</FvButton>
</template>

<script setup>
import { onMounted, ref, watch } from "vue";
import { useRoute } from "vue-router"
// import { useRouter } from "vue-router"
import { RecipeRequest } from "@/requests/recipe-request"
import { RecipeIngredientRequest } from "@/requests/recipe-ingredient-request"
import { RecipeInstructionRequest } from "@/requests/recipe-instruction-request"
import FvButton from "@/components/shared/FvButton.vue";


const route = useRoute()
// const router = useRouter()

const recipeRequest = new RecipeRequest()
const recipeIngredientRequest = new RecipeIngredientRequest()
const recipeInstructionRequest = new RecipeInstructionRequest()
const recipe = ref({})
const ingredients = ref([])
const instructions = ref([])
const recipeDescription = ref("")
const recipeName = ref("")


const props = defineProps({
  recipeId: {
    type: String,
    required: true
  }
});

onMounted(async () => {
  console.log(props.recipeId)
  recipe.value = await recipeRequest.getRecipeById(props.recipeId)
  recipeName.value = recipe.value.name
  recipeDescription.value = recipe.value.description;
  ingredients.value = await recipeIngredientRequest.getAllIngredientsForRecipe(props.recipeId)
  instructions.value = await recipeInstructionRequest.getAllInstructionsForRecipe(props.recipeId)
});

const updateRecipe = async function () {
  if (recipeName.value != recipe.value.name) {
    await recipeRequest.updateRecipeName(props.recipeId, recipeName.value)
  }
  if (recipeDescription.value != recipe.value.description) {
    await recipeRequest.updateRecipeDescription(props.recipeId, recipeDescription.value)
  }
}
watch(recipeDescription, () => {
});

</script>

<style scoped>
.recipeDescription {
  width: 400px;
  height: 25px;
  white-space: initial;
}
</style>
