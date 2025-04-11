<template>
  <div class="edit-group-user">
    <div>{{ user.firstName }} {{ user.lastName }}</div>
    <FvButton v-if="currentUserIsOwner" @click="editUserPermissions">Edit Permissions</FvButton>
    <EditGroupUserPermissionsModal
      v-if="showEditPermissionsModal"
      :user-id="user.userId"
      :group-id="groupId"
      @close="showEditPermissionsModal = false"
    />
  </div>
</template>

<script setup>
import FvButton from '@/components/shared/FvButton.vue'
import EditGroupUserPermissionsModal from './EditGroupUserPermissionsModal.vue'
import { ref } from 'vue'

const props = defineProps({
  currentUserIsOwner: {
    type: Boolean,
    required: true,
  },
  user: {
    type: Object,
    required: true
  },
  groupId: {
    type: String,
    required: true
  }
})

const showEditPermissionsModal = ref(false)

const editUserPermissions = () => {
  showEditPermissionsModal.value = true
}
</script>

<style scoped lang="scss">
.edit-group-user {
  display: flex;
  flex-direction: row;
  gap: 8px;
  align-items: center;
}
</style>