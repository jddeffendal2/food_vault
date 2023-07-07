<template>
  <div class="edit-recipes">
    <h2>Your Recipes:</h2>
    <RouterLink to="/CreateRecipe"><div class="create-new-recipe">Create New Recipe?</div></RouterLink>
    <div v-if="currentUserRecipes.length > 0">
      <div
        class="single-recipe"
        v-for="recipe in currentUserRecipes"
        :key="recipe.id" 
        @click="viewRecipe(recipe)"
      >
        <span class="recipe-name"> {{ recipe.name }}: &nbsp;&nbsp;</span>
        <span class="recipe-description">{{ recipe.description }}</span>
        <p class="last-updated"> Last Updated: {{ recipe.updatedDate }}</p>
      </div>
    </div>
    <div v-else>
      <br/><br/>
      <div class="no-recipes"><br/><br/>You have no recipes. <RouterLink class="create-recipe-link" to="/CreateRecipe">Create A Recipe?</RouterLink></div>
    </div>
    <br/>
  </div>
</template>

<script setup>
import { onMounted, ref } from "vue";
import { useAccountStore } from "@/stores/accountStore";
import { useRouter } from "vue-router";
import RecipeRequest from "@/requests/recipe-request";

const router = useRouter();

const accountStore = useAccountStore();

var currentUserRecipes = ref([]);

onMounted(async () => {
  currentUserRecipes.value = await new RecipeRequest().getUserRecipes(accountStore.currentUserId);
});

const viewRecipe = function (recipe) {
  router.push("/Recipe/" + recipe.id);
};
</script>

<style scoped>
.edit-recipes {
  justify-content: center;
  text-align: center;
}
.recipe-name {
  font-weight: bold;
}
.single-recipe {
  border: 3px solid #c7d6d5;
  border-radius: 5px;
  margin-top: 3px;
  margin-bottom: 3px;
  margin-left: auto;
  margin-right: auto;
  max-width: 550px;
  min-width: 550px;
}
.single-recipe:hover {
  cursor: pointer;
  background-color: lightgray;
}

.last-updated {
  font-size: 60%;
  font-weight: bold;
}
.recipe-description {
  font-size: smaller;
}

.no-recipes {
  font-size: large;
}

.create-recipe-link {
  text-decoration: underline;
  font-weight: bold;
}

.create-new-recipe {
  text-decoration: underline;
  font-weight: bold;
  font-size: 12px;
}
</style>
