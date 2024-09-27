<template>
  <FvLoadingSpinner v-if="loading" />
  <div class="invitations-page">
    <br/>
    <div v-if="invitationStore.invitations.length === 1">** You have been invited to join 1 group. **</div>
    <div v-else-if="invitationStore.invitations.length > 1"> ** You have been invited to join {{ invitationStore.invitations.length }} groups. **</div>
    <br/>
    <h1 class="invitationsTitle">Invitations</h1>
    <div v-if="invitationStore.invitations.length > 0" class="invitationsList">
      <table class="invitations-table">
        <tr
          v-for="invitation in invitationStore.invitations"
          :key="invitation.invitationId"
          class="invite-row"
        >
          <td class="invite-name">{{ invitation.groupName }}</td>
          <td class="invite-about">
            <FvButton @click="aboutInvitation(invitation)">About</FvButton>
          </td>
          <td class="invite-accept">
            <FvButton @click="acceptInvitation(invitation)">Accept</FvButton>
          </td>
          <td class="invite-reject">
            <FvButton @click="rejectInvitation(invitation)">Reject</FvButton>
          </td>
        </tr>
      </table>
    </div>
    <div v-else>
      <br/><br/>
      <h3>You have no invitations.</h3>
    </div>
  </div>
  <AcceptedInvitationNotification v-if="isInvitationAccepted" @close="closeInvitationModal" />
  <InvitationInfoModal v-if="isAboutModalClicked" :invitationInformation="invitationInfo" @close="closeAboutInvitationModal" />
</template>

<script setup>
import { useAccountStore } from "@/stores/accountStore.js";
import { InvitationRequest } from "@/requests/invitation-request";
import FvButton from "@/components/shared/FvButton.vue";
import AcceptedInvitationNotification from "@/components/AcceptedInvitationNotification.vue";
import InvitationInfoModal from "@/components/InvitationInfoModal.vue";
import FvLoadingSpinner from "@/components/shared/FvLoadingSpinner.vue";
import { ref } from "vue";
import { useInvitationsStore } from "../stores/invitationsStore";

const accountStore = useAccountStore();
const invitationStore = useInvitationsStore()
const invitationRequest = new InvitationRequest();
const isInvitationAccepted = ref(false);
const isAboutModalClicked = ref(false);
const invitationInfo = ref({});
const loading = ref(false); 

const loadInvitations = async () => {
  await invitationStore.getInvitations(accountStore.currentUserId)
}

const aboutInvitation = function (invitation) {
  isAboutModalClicked.value = true;
  invitationInfo.value = invitation;
}

const closeAboutInvitationModal = async function() {
  isAboutModalClicked.value = false;
}

const acceptInvitation = async function (invitation) {
  await invitationRequest.acceptInvitation(invitation.invitationId);
  isInvitationAccepted.value = true;
  loading.value = true;
  await loadInvitations();
  loading.value = false;
}

const rejectInvitation = async function(invitation) {
  loading.value = true;
  await invitationRequest.rejectInvitation(invitation.invitationId);
  await loadInvitations();
  loading.value = false;
}

const closeInvitationModal = function() {
  isInvitationAccepted.value = false;
}
</script>

<style scoped>
.invitationsList {
  margin: auto auto;
  min-width: 700px;
  max-width: 700px;
  border-left: 2px solid #c7d6d5;
  border-right: 2px solid #c7d6d5;
  border-top: 2px solid #c7d6d5;
  border-radius: 5px;
}
.invite-name {
  width: 60%;
}
.invitations-table {
  width: 100%;
  border-collapse: collapse;
}
.invite-row {
    border-bottom: 2px solid #c7d6d5;
}
.invitations-page{
    text-align: center;
    color: #043565;
}
.invitationsTitle {
    color: #043565;
}
</style>
