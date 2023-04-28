<template>
  <header>
    <RouterLink to="/">
      <h1 class="brandName">Recipe Vault</h1>
    </RouterLink>
    <ul class="nav-bar">
      <li><RouterLink to="/">WORK</RouterLink></li>
      <li><RouterLink to="/">ABOUT</RouterLink></li>
      <li><RouterLink to="/">CONTACT</RouterLink></li>
    </ul>
    <!-- <RouterLink to="/">
      <button class="loginButton" type="button">Log In</button>
    </RouterLink> -->
  </header>
  <div>
    <RouterView />
  </div>
</template>

<script setup>
import { ref, onMounted } from "vue";
import { useAccountStore } from "@/stores/accountStore.js";
import router from "./router";

const accountStore = useAccountStore();
var loggedIn = ref(false);

onMounted(async () => {
  await accountStore.initialize();

  if (!accountStore.isLoggedIn) {
    router.push("/login");
  } else {
    router.push("/")
  }
});

// Potential colors: #FFFEF2 background white
// #5DD39E main green
// #087E8B secondary blue
</script>

<style scoped>
header {
  background-color: antiquewhite;
  max-height: 100px;
  border: 1px solid #E8E8E4;
  border-radius: 8px;
  box-shadow: 0 3px 3px #ECE4DB;
}

.brandName {
  color: #087E8B;
  padding-left: 100px;
}

a {
  text-decoration: none;
  color: #087E8B;
}
ul {
  list-style-type: none;
}

ul li {
  padding: 30px;
}

.nav-bar {
  display: flex;
  font-family: Poppins;
}

@media (min-width: 1024px) {
  header {
    display: flex;
    flex-direction: row;
    align-items: center;
    /* justify-content: space-between; */
  }
}
</style>
