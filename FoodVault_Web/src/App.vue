<template>
  <NavBar />
  <div class="page-wrapper">
    <RouterView />
  </div>
</template>

<script setup>
import { onBeforeMount, ref } from "vue";
import { useAccountStore } from "@/stores/accountStore.js";
import { useRouter } from "vue-router";
import NavBar from "@/components/NavBar.vue";

const router = useRouter();
const accountStore = useAccountStore();

const isNotificationsOpen = ref(false);

onBeforeMount(async () => {
  await accountStore.initialize();

  if (!accountStore.isLoggedIn) {
    router.push("/LandingPage");
  }
});

// Potential colors: #FFFEF2 background white
// #5DD39E main green
// #087E8B secondary blue
</script>

<style>
@import url('https://fonts.googleapis.com/css2?family=Quicksand&display=swap');

html, body {
  font-family: 'Quicksand', sans-serif;
}

#app {
  font-family: 'Quicksand', sans-serif;
}

a {
  text-decoration: none;
  color: #043565;
}

.page-wrapper {
  margin-left: 12px;
  margin-top: 12px;
}

/* header {
  background-color: #C7D6D5;
  max-height: 100px;
  border: 1px solid #E8E8E4;
  border-radius: 8px;
  box-shadow: 0 3px 3px #ECE4DB;
} */
</style>
