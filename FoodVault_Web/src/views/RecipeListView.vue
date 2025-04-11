<template>
  <div>
    <select v-model="recipeFilter">
      <option value="0">All Recipes</option>
      <option value="1">Recipes You Own</option>
      <option value="2">Recipes Shared With You</option>
    </select>
    <h2>Your Recipes:</h2>
    <RouterLink to="/CreateRecipe" v-if="allRecipesVisibleToUser.length > 0">
      <div class="create-recipe-link">Create New Recipe</div>
    </RouterLink>
    <div v-if="allRecipesVisibleToUser.length > 0">
      <div class="single-recipe" v-for="recipe in recipesToDisplay" :key="recipe.id" @click="viewRecipe(recipe)">
        <span class="recipe-name"> {{ recipe.name }}: &nbsp;&nbsp;</span>
        <span class="recipe-description">{{ recipe.description }}</span>
        <div class="last-updated"> Last Updated: {{ formatDate(recipe.updatedDate) }}</div>
      </div>
    </div>
    <div v-else>
      <br /><br />
      <div class="no-recipes"><br /><br />You have no recipes. <RouterLink class="create-recipe-link"
          to="/CreateRecipe">Create A Recipe?</RouterLink>
      </div>
    </div>
    <br />
  </div>
</template>

<script setup>
import { computed, onMounted, ref } from "vue";
import { RecipeRequest } from "@/requests/recipe-request";
import { useAccountStore } from "@/stores/accountStore";
import { useRouter } from "vue-router";
import { formatDate } from "@/utils/date-utils";

const router = useRouter();
const accountStore = useAccountStore();

const recipeFilter = ref('0')
const allRecipesVisibleToUser = ref([]);

const recipesToDisplay = computed(() => {
  if (recipeFilter.value === '0') {
    // Show all recipes
    return allRecipesVisibleToUser.value
  } else if (recipeFilter.value === '1') {
    // Only show recipes owned by user
    return allRecipesVisibleToUser.value.filter(x => x.userId === accountStore.currentUserId)
  } else if (recipeFilter.value === '2') {
    // Only show recipes shared with user (AKA you don't own)
    return allRecipesVisibleToUser.value.filter(x => x.userId !== accountStore.currentUserId)
  }
})

onMounted(async () => {
  allRecipesVisibleToUser.value = await new RecipeRequest().getAllRecipesUserCanView(accountStore.currentUserId)
});

const viewRecipe = function (recipe) {
  router.push("/Recipe/" + recipe.id);
};
</script>

<style scoped>
.recipe-name {
  font-weight: bold;
}

.single-recipe {
  border: 3px solid #c7d6d5;
  border-radius: 8px;
  margin: 4px 0;
  padding: 8px;
  max-width: 550px;
  min-width: 550px;
}

.single-recipe:hover {
  cursor: pointer;
  background-color: lightgray;
}

.last-updated {
  padding-top: 8px;
  font-size: 14px;
  font-weight: bold;
}

.recipe-description {
  font-size: 16px;
}

.no-recipes {
  font-size: large;
}

.create-recipe-link {
  font-size: 16px;
  text-decoration: underline;
  font-weight: bold;
}
</style>