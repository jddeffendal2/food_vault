<template>
  <div class="editRecipes">
    <h2>Edit Your Recipes:</h2>
    <div v-if="currentUserRecipes.length > 0">
      <div
        class="singleRecipe"
        v-for="recipe in currentUserRecipes"
        :key="recipe.id" 
        @click="editRecipe(recipe)"
      >
        <span class="recipeName"> {{ recipe.name }}: &nbsp;&nbsp;</span>
        <span class="recipeDescription">{{ recipe.description }}</span>
        <p class="lastUpdated"> Last Updated: {{ recipe.updatedDate }}</p>
      </div>
    </div>
    <div v-else>
      <br/><br/>
      <div class="noRecipes"><br/><br/>You have no recipes. <RouterLink class="createRecipeLink" to="/CreateRecipe">Create A Recipe?</RouterLink></div>
    </div>
    <br/>
  </div>
</template>

<script setup>
import { onMounted, ref } from "vue";
import { useAccountStore } from "../stores/accountStore";
import { useRouter } from "vue-router";
import RecipeRequest from "@/requests/recipe-request";

const router = useRouter();

const accountStore = useAccountStore();

var currentUserRecipes = ref([]);

onMounted(async () => {
  currentUserRecipes.value = await new RecipeRequest().getUserRecipes(accountStore.currentUserId);
});

const editRecipe = function (recipe) {
  router.push({
    name: "EditSingleRecipe",
    params: {
      recipeId: recipe.id
    }
  });
};
</script>

<style scoped>
.editRecipes {
  justify-content: center;
  text-align: center;
}
.recipeName {
  font-weight: bold;
}
.singleRecipe {
  border: 3px solid #c7d6d5;
  border-radius: 5px;
  margin-top: 3px;
  margin-bottom: 3px;
  margin-left: auto;
  margin-right: auto;
  max-width: 550px;
  min-width: 550px;
}
.singleRecipe:hover {
  cursor: pointer;
  background-color: lightgray;
}

.lastUpdated {
  font-size: 60%;
  font-weight: bold;
}
.recipeDescription {
  font-size: smaller;
}

.noRecipes {
  font-size: large;
}

.createRecipeLink {
  text-decoration: underline;
  font-weight: bold;
}
</style>
