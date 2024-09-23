<template>
  <header>
    <div class="nav-bar">
      <div class="nav-bar__left">
        <RouterLink v-if="!accountStore.isLoggedIn" class="nav-item" to="/LandingPage">
          <h1 class="brand-name"><span>Food</span><span id="secondWord">Vault</span></h1>
        </RouterLink>
        <RouterLink v-if="accountStore.isLoggedIn" class="nav-item" to="/">
          <h1 class="brand-name"><span>Food</span><span id="secondWord">Vault</span></h1>
        </RouterLink>
        <div v-if="accountStore.isLoggedIn">
            <RouterLink class="hideable-nav-items" to="/Recipes">Recipes</RouterLink>
            <RouterLink class="hideable-nav-items" to="/Groups">Groups</RouterLink>
            <RouterLink class="hideable-nav-items" to="/Invitations">Invitations</RouterLink>
        </div>
      </div>
      <div class="nav-bar__middle">
      </div>
      <div class="nav-bar__right">
        <a v-if="accountStore.isLoggedIn" id="collapsedNavBarIcon" @click="expandNavBar">
            &equiv;
          </a>
        <div
          v-if="accountStore.isLoggedIn"
          class="hideable-nav-items notification"
          @click="openNotifications"
        >
          <span>
            <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-bell"
              viewBox="0 0 16 16">
              <path
                d="M8 16a2 2 0 0 0 2-2H6a2 2 0 0 0 2 2zM8 1.918l-.797.161A4.002 4.002 0 0 0 4 6c0 .628-.134 2.197-.459 3.742-.16.767-.376 1.566-.663 2.258h10.244c-.287-.692-.502-1.49-.663-2.258C12.134 8.197 12 6.628 12 6a4.002 4.002 0 0 0-3.203-3.92L8 1.917zM14.22 12c.223.447.481.801.78 1H1c.299-.199.557-.553.78-1C2.68 10.2 3 6.88 3 6c0-2.42 1.72-4.44 4.005-4.901a1 1 0 1 1 1.99 0A5.002 5.002 0 0 1 13 6c0 .88.32 4.2 1.22 6z" />
            </svg>
          </span>
          <span class="badge">{{ invitations.length }}</span>
        </div>
        <button
          v-if="accountStore.isLoggedIn"
          class="hideable-nav-items logout-button"
          type="button"
          @click="logOut"
        >
          Log Out
        </button>
        <button class="login-button" v-if="!accountStore.isLoggedIn" @click="logIn">
          Login
        </button>
      </div>
    </div>
    <div class="expanded-nav-bar" v-if="isExpanded == true">
      <RouterLink to="/">
        <h1 class="brand-name2">Food Vault 
      </h1>
      </RouterLink>
      <a id="expandedNavBarIcon" @click="closeNavbar">
        &equiv;
      </a>
      <br/><br/><br/><br/><br/>
      <div v-if="accountStore.isLoggedIn">
        <RouterLink @click="closeNavbar()" to="/Recipes">Recipes</RouterLink><br/><br/>
        <RouterLink @click="closeNavbar()" to="/Groups">Groups</RouterLink><br/><br/>
        <RouterLink @click="closeNavbar()" to="/Invitations">Invitations</RouterLink><br/><br/>
        <div
          class="notification2"
          @click="openNotifications"
        >
          <span>
            <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-bell"
              viewBox="0 0 16 16">
              <path
                d="M8 16a2 2 0 0 0 2-2H6a2 2 0 0 0 2 2zM8 1.918l-.797.161A4.002 4.002 0 0 0 4 6c0 .628-.134 2.197-.459 3.742-.16.767-.376 1.566-.663 2.258h10.244c-.287-.692-.502-1.49-.663-2.258C12.134 8.197 12 6.628 12 6a4.002 4.002 0 0 0-3.203-3.92L8 1.917zM14.22 12c.223.447.481.801.78 1H1c.299-.199.557-.553.78-1C2.68 10.2 3 6.88 3 6c0-2.42 1.72-4.44 4.005-4.901a1 1 0 1 1 1.99 0A5.002 5.002 0 0 1 13 6c0 .88.32 4.2 1.22 6z" />
            </svg>
          </span>
          <span class="badge2">{{ invitations.length }}</span>
        </div>
        <br/>
        <button
          class="logout-button"
          type="button"
          @click="logOut(); closeNavbar()"
        >
          Log Out
        </button>
      </div>
      <br/>
    </div>
    <NotificationsModal
      v-if="isNotificationsOpen"
      @open="isNotificationsOpen = true"
      @close="isNotificationsOpen = false"
    />
  </header>
</template>

<script setup>
import { onMounted, ref } from "vue";
import { useAccountStore } from "@/stores/accountStore.js";
import { InvitationRequest } from "@/requests/invitation-request";
import NotificationsModal from "@/components/NotificationsModal.vue";
import { RouterLink, useRouter } from "vue-router";

const accountStore = useAccountStore();
const router = useRouter();

const emit = defineEmits(["open"]);

const invitations = ref([]);
const isExpanded = ref(false)
const isNotificationsOpen = ref(false);

function logOut() {
  accountStore.logOut();
}
function logIn() {
  router.push("/Login");
}
const openNotifications = function() {
  isNotificationsOpen.value = true;
}
let expandNavBar = function () {
  isExpanded.value = true;
}
let closeNavbar = function () {
  isExpanded.value = false;
}

onMounted(async () => {
  if (accountStore.isLoggedIn) {
    invitations.value = await new InvitationRequest().getInvitationsToUser(accountStore.currentUserId);
  }
})
</script>

<style scoped lang="scss">
.nav-bar {
  box-sizing: border-box;
  background-color: #C7D6D5;
  box-shadow: 0 3px 3px #ECE4DB;
  display: flex;
  min-height: 75px;
  max-height: 100px;
  gap: 0;
  border-radius: 8px;
  padding: 8px 24px;
  width: 100%;


  &__left {
    align-content: center;
    align-items: center;
    display: flex;
    justify-content: flex-start;
  }

  &__middle {
    align-content: center;
    align-items: center;
    display: flex;
    flex-grow: 1;
    justify-content: center;
  }

  &__right {
    align-content: center;
    align-items: center;
    display: flex;
    justify-content: flex-end;
  }
}

.nav-item {
  margin-left: 16px;
  margin-right: 16px;
}

.hideable-nav-items {
  margin-left: 16px;
}

.brand-name {
  color: #043565;
}
.brand-name2 {
  color: #043565; 
  float: left;
}

#secondWord {
    color: white;
}

.logout-button, .login-button {
  min-width: 100px;
  max-width: 100px;
  min-height: 40px;
  max-height: 40px;
  border-radius: 8px;
  border: 1px solid #043565;
  color: #043565;
}

.logout-button:hover, .login-button:hover {
  box-shadow: 1px 1px 1px gray;
  cursor: pointer;
}

.notification {
  text-decoration: none;
  position: relative;
}

.notification2 {
  text-decoration: none;
  position: relative;
}

.notification2:hover {
  .badge2{
    background-color: red;
  }
  cursor: pointer;
}

.notification:hover {
  .badge{
    background-color: red;
  }
  cursor: pointer;
}

.badge2:hover {
  background-color: red;
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

.badge2 {
  position: relative;
  top: -11px;
  right: 6px;
  padding: 0px 4px;
  border-radius: 50%;
  background-color: #043565;
  color: white;
  font-size: 10px;
}

#collapsedNavBarIcon {
  transform: scale(1.5, 1.5);
  visibility: hidden;
}

#collapsedNavBarIcon:hover {
  cursor: pointer;
}

#expandedNavBarIcon {
  float: right;
  transform: scale(1.5, 1.5);
  margin-top: 30px;
}

#expandedNavBarIcon:hover {
  cursor: pointer;
}


.expanded-nav-bar {
  box-sizing: border-box;
  background-color: #C7D6D5;
  box-shadow: 0 3px 3px #ECE4DB;
  height: 320px;
  border-radius: 8px;
  padding: 8px 24px;
  visibility: hidden;
  top: 1px;
  position: fixed;
  width: 97%;
  margin-top: 7px;
}

@media screen and (max-width: 632px) {
  .hideable-nav-items{
    visibility: hidden;
  }
  #collapsedNavBarIcon {
    visibility: visible;
  }

  .expanded-nav-bar {
    visibility: visible;
  }
}

@media screen and (max-width: 250px) {
  .brand-name2 {
    visibility: hidden;
  }
}
</style>