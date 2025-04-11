<template>
  <FvModal>
    <template #header>
      Edit User's Permissions
    </template>
    <div v-show="!loading" class="edit-permissions">
      <div>
        <input type="checkbox" id="can-add-users" v-model="canAddUsers"/>
        <label for="can-add-users">Can Add Users</label>
      </div>
      <div>
        <input type="checkbox" id="can-add-recipes" v-model="canAddRecipes"/>
        <label for="can-add-recipes">Can Add Recipes</label>
      </div>
    </div>
    <template #footer>
      <div class="modal-actions">
        <FvButton @click="close">Cancel</FvButton>
        <FvButton @click="savePermissions">Save</FvButton>
      </div>
    </template>
  </FvModal>
</template>

<script setup>
import { onMounted, ref } from 'vue'
import { UserGroupRequest } from '../requests/user-group-request'
import FvButton from '@/components/shared/FvButton.vue'
import FvModal from '@/components/shared/FvModal.vue'

const props = defineProps({
  userId: {
    type: String,
    required: true
  },
  groupId: {
    type: String,
    required: true
  }
})

const emit = defineEmits(['close'])

const userGroupRequest = new UserGroupRequest()

const loading = ref(true)
const userGroupInfo = ref()
const canAddUsers = ref(false)
const canAddRecipes = ref(false)

const close = () => {
  if (loading.value) return
  emit('close')
}

const savePermissions = async () => {
  loading.value = true
  const saveResult = await userGroupRequest.updateUserGroupPermissions(props.userId, props.groupId, canAddRecipes.value, canAddUsers.value)
  // todo: handle failure
  loading.value = false
  close()
}

onMounted(async () => {
  loading.value = true

  const userGroup = await userGroupRequest.getUserGroup(props.groupId, props.userId)
  userGroupInfo.value = userGroup
  canAddUsers.value = userGroup.canAddUsers
  canAddRecipes.value = userGroup.canAddRecipes
  
  loading.value = false
})
</script>

<style scoped lang="scss">
.edit-permissions {
  display: flex;
  flex-direction: column;
  gap: 12px;
}

.modal-actions {
  display: flex;
  flex-direction: row;
  gap: 12px;
  justify-content: end;
}
</style>