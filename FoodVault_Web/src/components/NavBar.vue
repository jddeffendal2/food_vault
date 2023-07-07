<template>
  <header>
    <div class="nav-bar">
      <div class="nav-bar__left">
        <RouterLink class="nav-item" to="/">
          <h1 class="brand-name">Food Vault</h1>
        </RouterLink>
        <div v-if="accountStore.isLoggedIn">
          <RouterLink class="nav-item" to="/Recipes">Recipes</RouterLink>
          <RouterLink class="nav-item" to="/Groups">Groups</RouterLink>
          <RouterLink class="nav-item" to="/Invitations">Invitations</RouterLink>
        </div>
      </div>
      <div class="nav-bar__middle"></div>
      <div class="nav-bar__right">
        <div
          v-if="accountStore.isLoggedIn"
          class="nav-item notification"
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
          class="nav-item logout-button"
          type="button"
          @click="logOut"
        >
          Log Out
        </button>
      </div>
    </div>
  </header>
</template>

<script setup>
import { onMounted, ref } from "vue";
import { useAccountStore } from "@/stores/accountStore.js";
import InvitationRequest from "@/requests/invitation-request";

const accountStore = useAccountStore();

const emit = defineEmits(["open"]);

const invitations = ref([]);

function logOut() {
  accountStore.logOut();
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

.brand-name {
  color: #043565;
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