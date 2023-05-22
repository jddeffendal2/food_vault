<template>
  <div class="editRecipes">
    <h2>Edit Your Recipes:</h2>
    <div>
      <div
        class="singleRecipe"
        v-for="recipe in currentUserRecipes"
        :key="recipe.id"
      >
        <span class="recipeName"> {{ recipe.name }}: &nbsp;&nbsp;</span>
        <span>{{ recipe.description }}</span>
        <br />
      </div>
    </div>
    <br/>
  </div>
</template>

<script setup>
import { onMounted, ref } from "vue";
import { useAccountStore } from "../stores/accountStore";
import { getRecipesByUserId } from "../utils/request.js";

const accountStore = useAccountStore();

var currentUserRecipes = ref([]);

onMounted(async () => {
  currentUserRecipes.value = await getRecipesByUserId(
    accountStore.currentUserId,
    (res) => res
  );
});
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
</style>
