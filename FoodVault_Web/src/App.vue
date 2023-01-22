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
    <h1>Recipe Vault</h1>
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
}

.logo {
  display: block;
  margin: 0 auto 2rem;
}

nav {
  width: 100%;
  font-size: 12px;
  text-align: center;
  margin-top: 2rem;
}

nav a.router-link-exact-active {
  color: var(--color-text);
}

nav a.router-link-exact-active:hover {
  background-color: transparent;
}

nav a {
  display: inline-block;
  padding: 0 1rem;
  border-left: 1px solid var(--color-border);
}

nav a:first-of-type {
  border: 0;
}

@media (min-width: 1024px) {
  header {
    display: flex;
    place-items: center;
    padding-right: calc(var(--section-gap) / 2);
  }

  .logo {
    margin: 0 2rem 0 0;
  }

  header .wrapper {
    display: flex;
    place-items: flex-start;
    flex-wrap: wrap;
  }

  nav {
    text-align: left;
    margin-left: -1rem;
    font-size: 1rem;

    padding: 1rem 0;
    margin-top: 1rem;
  }
}
</style>
