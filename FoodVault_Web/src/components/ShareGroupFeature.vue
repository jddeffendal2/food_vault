<template>
  <div class="modal-background">
    <div class="modal">
      <header class="modal-header">
        Add people to this group to share your recipes with!
        <button type="button" class="btn-close" @click="close">X</button>
      </header>

      <section class="modal-body">
        <div>
          <input class="search-bar" type="search" v-model="searchedUser" placeholder="Add People to this Group"/>
          <div v-if="searchedUser.length > 0" class="user-search-results-box">
            <div v-for="user in searchResults" :key="user" class="user-search-result" @click="selectUser(user)">
              &nbsp;{{ user.firstName }} {{ user.lastName }} <br/>
              &nbsp;{{ user.username }}
              <br/><br/>
            </div>
          </div>
        </div>
        <br /><br />
        <div v-if="selectedUser">
          {{ selectedUser.username }}
        </div>
        <br /><br />
        <button type="button" class="invite-button" @click="invite">
          Invite
        </button>
      </section>
    </div>
  </div>
</template>

<script setup>
import { ref, watch } from "vue";
import { useRoute } from "vue-router"
import InvitationRequest from "@/requests/invitation-request";
import UserRequest from "@/requests/user-request";
import { useAccountStore } from "@/stores/accountStore";

const route = useRoute();
const accountStore = useAccountStore();
const invitationRequest = new InvitationRequest();
const userRequest = new UserRequest();

const searchedUser = ref("");
const searchResults = ref([]);
const selectedUser = ref(null);

const emit = defineEmits(["close"]);

const selectUser = function (user) {
  selectedUser.value = user;
  searchedUser.value = "";
}

const invite = async function () {
  await invitationRequest.sendRequest({
    fromUserId: accountStore.currentUserId,
    toUserId: selectedUser.value.userId,
    groupId: route.params.groupId
  });
  close();
};

let timeout = null;
watch(searchedUser, () => {
  if (searchedUser.value.length == 0) return;
  if (timeout) {
    clearTimeout(timeout)
  }
  timeout = window.setTimeout(async () => {
    searchResults.value = await userRequest.searchUsers(accountStore.currentUserId, searchedUser.value)
  }, 300);
});

const close = function () {
  emit("close");
}
</script>

<style scoped>
.modal-background {
  position: fixed;
  top: 0;
  bottom: 0;
  left: 0;
  right: 0;
  background-color: rgba(0, 0, 0, 0.3);
  display: flex;
  justify-content: center;
  align-items: center;
}

.modal {
  min-width: 550px;
  max-width: 550px;
  min-height: 400px;
  max-height: 400px;
  background: #ffffff;
  overflow-x: auto;
  display: flex;
  flex-direction: column;
  border-radius: 15px;
}
.modal-header {
  border-radius: 15px;
  padding: 15px;
  display: flex;
  background-color: #c7d6d5;
  position: relative;
  border-bottom: 1px solid #eeeeee;
  color: #043565;
  font-weight: bold;
  justify-content: center;
}

.modal-body {
  position: relative;
  padding: 20px 10px;
}

.btn-close {
  position: absolute;
  top: 0;
  right: 0;
  border: none;
  font-size: 20px;
  padding: 10px;
  cursor: pointer;
  font-weight: bold;
  color: #043565;
  background: transparent;
}

.invite-button {
  color: #043565;
  background: #c7d6d5;
  border: 1px solid #4aae9b;
  border-radius: 2px;
}

.invite-button:hover {
  cursor: pointer;
  box-shadow: 1px 1px 1px gray;
}

.search-bar {
  max-width: 400px;
  min-width: 400px;
}
.user-search-results-box {
  border: 2px solid #c7d6d5;
  border-radius: 5px;
  max-width: 400px;
  min-width: 400px;
  max-height: 100px;
  margin-right: auto;
  margin-left: auto;
  flex-wrap: wrap;
  overflow-y: auto;
  text-align: left;
}

.user-search-result {
  background-color: gray;
}
.user-search-result:hover {
  background-color: darkgray;
}
</style>
