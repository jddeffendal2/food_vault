import { ref, computed } from 'vue'
import { defineStore } from 'pinia'
import { getUser } from '../utils/request';

export const useAccountStore = defineStore("account", () => {
  const userId = ref(null);
  const token = ref(null);
  const user = ref({});

  const currentUserId = computed(() => userId.value);
  const activeToken = computed(() => token.value);
  const isLoggedIn = computed(() => userId.value !== null && token.value !== null && user.value !== null);

  async function initialize() {
    var localToken = localStorage.getItem("fv-token");
    var localUserId = localStorage.getItem("fv-userId");

    if (localToken) token.value = localToken;
    if (localUserId) userId.value = localUserId;

    if (userId.value && token.value) {
      await loadUserData(userId.value);
    }
  }

  async function logIn(userData) {
    localStorage.setItem("fv-token", userData.token);
    localStorage.setItem("fv-userId", userData.userId);
    token.value = userData.token;
    userId.value = userData.userId;

    await loadUserData(userId.value);
  }

  function logOut() {
    localStorage.removeItem("fv-token");
    localStorage.removeItem("fv-userId");
    clearUserInfo();
  }

  function loadUserData(userId) {
    return getUser(userId, (res) => {
      user.value = res;
    });
  }

  function clearUserInfo() {
    token.value = null;
    username.value = null;
    userId.value = null;
    userFullName.value = null;
  }

  return {
    userId,
    token,
    user,
    currentUserId,
    activeToken,
    isLoggedIn,
    initialize,
    logIn,
    logOut
  }
})