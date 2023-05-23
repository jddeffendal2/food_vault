<template>
  <h2>Edit This Recipe</h2>
</template>

<script setup>
import { onMounted } from "vue";

const props = defineProps({
  recipeId: {
    type: String,
    required: true
  }
});

onMounted(() => {
  console.log(props.recipeId)
});

</script>

<style scoped></style>
