<script setup>
import { onMounted } from "vue";
import LoginPage from "@/components/LoginPage.vue";
// Get token from local storage on mounted to check if logged in.
// If not logged in, show login page
// else show home page (TBD)
</script>

<template>
  <LoginPage />
</template>