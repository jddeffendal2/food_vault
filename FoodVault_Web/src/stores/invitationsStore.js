import { defineStore } from 'pinia'
import { ref } from 'vue'
import { InvitationRequest } from '../requests/invitation-request'

export const useInvitationsStore = defineStore('invitations', () => {
  const invitations = ref([])

  const getInvitations = async (userId) => {
    invitations.value = await new InvitationRequest().getInvitationsToUser(userId)
  }

  return { getInvitations, invitations }
})