<template>
  <h3>{{ recipe.name }}</h3>
  <p>{{ recipe.description }}</p>
  <button @click="editRecipe">Edit</button>
</template>

<script setup>
import { onMounted, ref } from "vue";
import { useRoute } from "vue-router";
import { useRouter } from "vue-router";
import RecipeRequest from "@/requests/recipe-request";

const route = useRoute();
const router = useRouter();

const recipeRequest = new RecipeRequest();

const recipe = ref({});

const editRecipe = function() {
  router.push({
    name: "EditSingleRecipe",
    params: {
      recipeId: recipe.value.id
    }
  });
}
onMounted(async () => {
  recipe.value = await recipeRequest.getRecipeById(route.params.id);
})
</script>

<style scoped></style>
