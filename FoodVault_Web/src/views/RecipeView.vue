<template>
  <FvLoadingSpinner v-if="loading" />
  <div v-else>
    <div v-if="!isUserAllowedToViewRecipe">
      <UnauthorizedRecipeViewerModal />
    </div>
    <div v-else>
      <FvButton v-if="isOwner" @click="editRecipe">Edit Recipe</FvButton>
      <div class="recipe">
        <div class="recipe__image">
          <img src="../assets/mac-and-cheese.jpg" alt="Recipe Image" width="350" height="350"/>
        </div>
        <div class="recipe__info">
          <h3 class="recipe__title">{{ recipe.name }}</h3>
          <div class="recipe__description">{{ recipe.description }} </div>
          <h3 class="recipe__ingredients">Ingredients</h3>
          <div
            v-for="ingredient in ingredients"
            :key="ingredient.ingredientId"
          >
            {{ ingredient.name }} - {{ ingredient.quantity }} {{ ingredient.unitOfMeasurement }}
          </div>
        </div>
      </div>
      <div class="recipe__instructions">
        <h3>Instructions</h3>
        <div v-for="instruction in instructions" :key="instruction.id">
          {{ instruction.sortOrder }}. {{ instruction.text }}
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { onMounted, ref } from "vue"
import { useAccountStore } from "@/stores/accountStore"
import { useRoute } from "vue-router"
import { useRouter } from "vue-router"
import { RecipeRequest } from "@/requests/recipe-request"
import { RecipeIngredientRequest } from "@/requests/recipe-ingredient-request"
import { RecipeInstructionRequest } from "@/requests/recipe-instruction-request"
import FvButton from "@/components/shared/FvButton.vue"
import FvLoadingSpinner from "@/components/shared/FvLoadingSpinner.vue"
import UnauthorizedRecipeViewerModal from "@/components/UnauthorizedRecipeViewerModal.vue"

const route = useRoute()
const router = useRouter()

const accountStore = useAccountStore()
const recipeRequest = new RecipeRequest()
const recipeIngredientRequest = new RecipeIngredientRequest()
const recipeInstructionRequest = new RecipeInstructionRequest()

const loading = ref(false)
const isOwner = ref(false)
const isUserAllowedToViewRecipe = ref(true)
const recipe = ref({})
const ingredients = ref([])
const instructions = ref([])

const editRecipe = function () {
  router.push({
    name: "EditSingleRecipe",
    params: {
      recipeId: recipe.value.id,
    },
  })
}

onMounted(async () => {
  loading.value = true
  isUserAllowedToViewRecipe.value = await recipeRequest.isUserAllowedToViewRecipe(accountStore.currentUserId, route.params.id)
  isOwner.value = await recipeRequest.isUserOwnerOfRecipe(route.params.id, accountStore.currentUserId)

  recipe.value = await recipeRequest.getRecipeById(route.params.id)
  ingredients.value = await recipeIngredientRequest.getAllIngredientsForRecipe(route.params.id)
  instructions.value = await recipeInstructionRequest.getAllInstructionsForRecipe(route.params.id)
  loading.value = false
})
</script>

<style scoped lang="scss">
.recipe {
  display: grid;
  grid-template-columns: repeat(auto-fill, minmax(350px, 1fr));
  column-gap: 24px;

  &__image {
    display: block;
  }

  &__info {
    display: block;
  }

  &__title {
    display: block;
    font-size: 35px;
    color: #043565;
    font-weight: bold;
    text-decoration: overline;
  }

  &__description {
    display: block;
  }

  &__ingredients {
    display: block;
    color: #043565;
  }

  &__instructions {
    grid-template-columns: 1fr;
    color: #043565;
  }
}

.button-div {
  flex: 30%;
  min-width: 100px;
  max-width: 100px;
  padding: 5px;
  min-height: 100px;
  border-radius: 8px;
  margin: 50px 4px 4px 4px;
  justify-content: center;
  text-align: center;
  /* border: 1px solid #043565; */
}

.bottom-div {
  /* border: 1px solid #043565; */
  
  margin: 4px 136px 4px 150px;
  text-align: center;
}
</style>
