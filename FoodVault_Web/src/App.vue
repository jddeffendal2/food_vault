<template>
  <header>
    <RouterLink to="/">
      <h1 class="brand-name">Food Vault</h1>
    </RouterLink>
    <ul class="nav-bar">
      <li><RouterLink to="/Groups">Recipes</RouterLink></li>
      <li><RouterLink to="/Groups">Groups</RouterLink></li>
      <li>Invitations</li>
      <li></li>
      <li></li>
      <li></li>
      <li></li>
      <li></li>
      <li></li>
      <li>
        <div class="notification" @click="openNotifications">
          <span>
            <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-bell" viewBox="0 0 16 16">
              <path d="M8 16a2 2 0 0 0 2-2H6a2 2 0 0 0 2 2zM8 1.918l-.797.161A4.002 4.002 0 0 0 4 6c0 .628-.134 2.197-.459 3.742-.16.767-.376 1.566-.663 2.258h10.244c-.287-.692-.502-1.49-.663-2.258C12.134 8.197 12 6.628 12 6a4.002 4.002 0 0 0-3.203-3.92L8 1.917zM14.22 12c.223.447.481.801.78 1H1c.299-.199.557-.553.78-1C2.68 10.2 3 6.88 3 6c0-2.42 1.72-4.44 4.005-4.901a1 1 0 1 1 1.99 0A5.002 5.002 0 0 1 13 6c0 .88.32 4.2 1.22 6z"/>
            </svg>
          </span>
          <span class="badge">9</span>
        </div>
      </li>
    </ul>
    <button v-if="accountStore.isLoggedIn" class="logout-button" type="button" @click="logOut">Log Out</button>
    <NotificationsModal v-if="isNotificationsOpen" @close="closeNotificationsModal"></NotificationsModal>
  </header>
  <div>
    <RouterView />
  </div>
</template>

<script setup>
import { onMounted, ref } from "vue";
import { useAccountStore } from "@/stores/accountStore.js";
import router from "./router";
import NotificationsModal from "@/components/NotificationsModal.vue";

const accountStore = useAccountStore();
const isNotificationsOpen = ref(false);

onMounted(async () => {
  await accountStore.initialize();

  if (!accountStore.isLoggedIn) {
    router.push("/login");
  }
});

function logOut() {
  accountStore.logOut()
}

const openNotifications = function() {
  isNotificationsOpen.value = true;
}

const closeNotificationsModal = function() {
  isNotificationsOpen.value = false;
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
  }
}

.notification {
  text-decoration: none;
  position: relative;
}
.notification:hover {
  .badge{
    background-color: red;
  }
  cursor: pointer;
}

.badge {
  position: absolute;
  top: -8px;
  right: -7px;
  padding: 0px 4px;
  border-radius: 50%;
  background-color: #043565;
  color: white;
  font-size: 10px;
}

</style>
