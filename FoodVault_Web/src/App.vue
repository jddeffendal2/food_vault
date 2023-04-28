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
    <RouterLink v-if="accountStore.isLoggedIn" to="/login">
      <button class="logoutButton" type="button" @click="logOut">Log Out</button>
    </RouterLink>
  </header>
  <div>
    <RouterView />
  </div>
</template>

<script setup>
import { onMounted } from "vue";
import { useAccountStore } from "@/stores/accountStore.js";
import router from "./router";

const accountStore = useAccountStore();

onMounted(async () => {
  await accountStore.initialize();

  if (!accountStore.isLoggedIn) {
    router.push("/login");
  } else {
    router.push("/")
  }
});

function logOut() {
  accountStore.logOut()
}

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

.logoutButton {
  margin: auto auto auto 400px;
  min-width: 100px;
  max-width: 100px;
  min-height: 40px;
  max-height: 40px;
  border-radius: 8px;
  border: 1px solid #087E8B;
  color: #087E8B;
}

.logoutButton:hover {
  box-shadow: 1px 1px 1px gray;
  cursor: pointer;
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
