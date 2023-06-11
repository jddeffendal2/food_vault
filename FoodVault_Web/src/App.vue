<template>
  <header>
    <RouterLink to="/">
      <h1 class="brand-name">Food Vault</h1>
    </RouterLink>
    <ul class="nav-bar">
      <li><RouterLink to="/Groups">Groups</RouterLink></li>
      <li>Invitations</li>
    </ul>
    <button v-if="accountStore.isLoggedIn" class="logout-button" type="button" @click="logOut">Log Out</button>
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
  }
});

function logOut() {
  accountStore.logOut()
}

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

header {
  background-color: #C7D6D5;
  max-height: 100px;
  border: 1px solid #E8E8E4;
  border-radius: 8px;
  box-shadow: 0 3px 3px #ECE4DB;
}

.brand-name {
  color: #043565;
  padding-left: 100px;
}

a {
  text-decoration: none;
  color: #043565;
}
ul {
  list-style-type: none;
}

ul li {
  padding: 30px;
}

.nav-bar {
  display: flex;
}

.logout-button {
  margin: auto auto auto 400px;
  min-width: 100px;
  max-width: 100px;
  min-height: 40px;
  max-height: 40px;
  border-radius: 8px;
  border: 1px solid #043565;
  color: #043565;
}

.logout-button:hover {
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
