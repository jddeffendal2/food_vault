// import { ref, computed } from 'vue'
import { defineStore } from 'pinia'

export const useAccountStore = defineStore('account', {
  state: () => ({
    loggedIn: false,
    user: undefined,
  }),
  getters: {
    user: (state) => state.user,
  },
  actions: {},
})