<script>
import { useAccountStore } from "@/stores/accountStore.js";

// Potential colors: #FFFEF2 background white
// #5DD39E main green
// #087E8B secondary blue

export default {
  setup() {
    const userStore = useAccountStore();

    return { userStore }
  }
}
</script>

<template>
  <header>
    <router-link to="/">
      <h1 class="brandName">Recipe Vault</h1>
    </router-link>
    <ul class="nav-bar">
      <li><router-link to="/">WORK</router-link></li>
      <li><router-link to="/">ABOUT</router-link></li>
      <li><router-link to="/">CONTACT</router-link></li>
    </ul>
    <router-link to="/">
      <button class="loginButton" type="button">Log In</button>
    </router-link>
  </header>
  <div>
    <router-view></router-view>
    <!-- Todo: show either home page or login page after checking if should be logged in -->
  </div>
</template>

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
