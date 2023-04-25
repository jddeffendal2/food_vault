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
  line-height: 1.5;
  max-height: 100vh;
  background-color: antiquewhite;
  text-align: center;
  border: 1px solid #E8E8E4;
  border-radius: 10px;
  box-shadow: 0 5px 10px #ECE4DB;
}

.brandName {
  color: #087E8B
}

ul {
  list-style-type: none;
}

a {
  text-decoration: none;
  color: #087E8B;
}

li {
  padding: 20px;
}

.nav-bar {
  font-family: Poppins;
  color: aqua;
  display: flex;
}

@media (min-width: 1024px) {
  header {
    display: flex;
    flex-direction: row;
    align-items: center;
    /* justify-content: space-between; */
  }

  header .wrapper {
    display: flex;
    place-items: flex-start;
    flex-wrap: wrap;
  }
}
</style>
