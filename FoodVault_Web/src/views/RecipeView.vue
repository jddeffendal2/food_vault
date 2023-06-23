<template>
  <h3>{{ recipe.name }}</h3>
  <p>{{ recipe.description }}</p>
</template>

<script setup>
import { onMounted, ref } from "vue";
import { useRoute } from "vue-router";
import RecipeRequest from "@/requests/recipe-request";

const route = useRoute();

const recipeRequest = new RecipeRequest();

const recipe = ref({});

onMounted(async () => {
  recipe.value = await recipeRequest.getRecipeById(route.params.id);
})
</script>

<style scoped></style>
