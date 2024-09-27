import { ref, computed } from 'vue'
import { defineStore } from 'pinia'
import { UserRequest } from "@/requests/user-request";
import router from "@/router";
import { UserGroupRequest } from '../requests/user-group-request';
import { useSignalrStore } from './signalrStore';
import { useInvitationsStore } from './invitationsStore';

export const useAccountStore = defineStore("account", () => {
  const userId = ref(null);
  const token = ref(null);
  const user = ref({});
  const userRequest = new UserRequest();

  const tokenKey = "fv-token";
  const userIdKey = "fv-userId";

  const currentUser = computed(() => user.value);
  const currentUserId = computed(() => userId.value);
  const activeToken = computed(() => token.value);
  const isLoggedIn = computed(() => userId.value !== null && token.value !== null && user.value !== null);

  async function initialize() {
    var localToken = localStorage.getItem(tokenKey);
    var localUserId = localStorage.getItem(userIdKey);

    if (localToken) token.value = localToken;
    if (localUserId) userId.value = localUserId;

    if (userId.value && token.value) {
      await loadUserData(userId.value);
    }
  }

  async function logIn(userInfo) {
    const response = await userRequest.logIn(userInfo);
    localStorage.setItem(tokenKey, response.token);
    localStorage.setItem(userIdKey, response.userId);
    token.value = response.token;
    userId.value = response.userId;

    await loadUserData(userId.value);
  }

  function logOut() {
    localStorage.removeItem(tokenKey);
    localStorage.removeItem(userIdKey);
    clearAccountStore();
    router.push("/LandingPage");
  }

  async function loadUserData(userId) {
    user.value = await userRequest.getUser(userId);
    const groupIdsUserIsPartOf = await new UserGroupRequest().getAllGroupsUserIsIn(userId);
    const signalR = useSignalrStore()
    await signalR.startConnection()
    if (groupIdsUserIsPartOf.length > 0)
      signalR.connectToAllGroups(groupIdsUserIsPartOf);
    
    
    useInvitationsStore().getInvitations(currentUserId.value)
  }


  function clearAccountStore() {
    token.value = null;
    userId.value = null;
    user.value = null;
  }

  return {
    currentUser,
    currentUserId,
    activeToken,
    isLoggedIn,
    initialize,
    logIn,
    logOut
  }
})