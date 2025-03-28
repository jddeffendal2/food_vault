<template>
  <FvLoadingSpinner v-if="loading" />
  <div class="tab-bar">
    <div
      @click="activeTab = 0; loadData();"
      :class="{ 'tab-button': true, 'active-tab': activeTab === 0, 'inactive-tab': activeTab !== 0}"
    >
      Recipes
    </div>
    <div
      :class="{ 'tab-button': true, 'active-tab': activeTab === 1, 'inactive-tab': activeTab !== 1}"
      @click="activeTab = 1"
    >
      Members
    </div>
    <div
      v-if="currentUserIsGroupOwner"
      @click="activeTab = 2"
      :class="{ 'tab-button': true, 'active-tab': activeTab === 2, 'inactive-tab': activeTab !== 2}"
    >
      Edit
    </div>
  </div>
  <div v-if="activeTab === 0">
    <FvButton v-if="currentUserIsGroupOwner || canAddRecipes" @click="showAddRecipeModal = true">Add Recipe</FvButton>
    <div v-if="recipes.length == 0">
      <br/><br/>
      There are no recipes in this group
    </div>
    <div v-else class="group-body">
      <div v-for="recipe in recipes" :key="recipe.id" class="recipe-card" @click="openRecipe(recipe)">
        {{ recipe.name }}
      </div>
    </div>
  </div>
  <div v-if="activeTab === 1" class="group-body">
    <div class="member-card">
      <FvButton v-if="currentUserIsGroupOwner || canAddUsers" @click="showInvitationModal = true">Invite Members</FvButton>
      <br/>
      <div>Owner: {{ owner.firstName }} {{ owner.lastName }}</div>
      <br />
      <div class="member-card__header">Members:</div>
      <table>
        <tr v-for="member in members" :key="member.userId">
          <td>{{ member.firstName }} {{ member.lastName }}</td>
          <td v-if="currentUserIsGroupOwner">
            <FvButton @click="removeUserFromGroup(member)">Remove</FvButton>
          </td>
        </tr>
      </table>
    </div>
  </div>
  <div v-if="activeTab === 2">
    <EditGroup :group-id="groupId" :members="members" :current-user-is-owner="currentUserIsGroupOwner" />
  </div>
  <AddRecipesToGroup
    v-if="showAddRecipeModal"
    :selected-group="groupInfoObject"
    @close="showAddRecipeModal = false"
  />
  <ShareGroupFeature
    v-if="showInvitationModal"
    :group-id="groupId"
    :shared-users="usersInGroupsIds"
    @close="showInvitationModal = false"
  />
</template>

<script setup>
import { computed, onMounted, ref } from "vue";
import { useRoute, useRouter } from "vue-router";
import { GroupRequest } from "@/requests/group-request";
import { GroupRecipeRequest } from "@/requests/group-recipe-request";
import { UserGroupRequest } from "@/requests/user-group-request";
import { useAccountStore } from "@/stores/accountStore"
import AddRecipesToGroup from "@/components/AddRecipesToGroup.vue";
import EditGroup from "@/components/EditGroup.vue";
import FvButton from "@/components/shared/FvButton.vue";
import FvLoadingSpinner from "@/components/shared/FvLoadingSpinner.vue";
import ShareGroupFeature from "@/components/ShareGroupFeature.vue";

const route = useRoute();
const router = useRouter();
const accountStore = useAccountStore();
const groupRequest = new GroupRequest();
const groupRecipeRequest = new GroupRecipeRequest();
const userGroupRequest = new UserGroupRequest();

const loading = ref(false);
const activeTab = ref(0);
const owner = ref({});
const members = ref([]);
const recipes = ref([]);
const canAddUsers = ref(false)
const canAddRecipes = ref(false)
const groupInfoObject = ref({})

const showAddRecipeModal = ref(false)
const showInvitationModal = ref(false)

const groupId = computed(() => route.params.id)
const currentUserIsGroupOwner = computed(() => owner.value.userId == accountStore.currentUserId)
const usersInGroupsIds = computed(() => [...members.value.map(x => x.userId), owner.value.userId])

const openRecipe = function (recipe) {
  router.push("/Recipe/" + recipe.id);
}

const removeUserFromGroup = async (member) => {
  loading.value = true;
  await userGroupRequest.removeUserFromGroup(groupId.value, member.userId);
  await loadData();
  loading.value = false;
}

const getGroupInfo = async () => {
  const groupInfo = await groupRequest.getGroupById(groupId.value)
  groupInfoObject.value = groupInfo
  owner.value = groupInfo.owner
  members.value = groupInfo.members.filter(x => x.userId !== accountStore.currentUserId)
}

const getGroupRecipes = async () => {
  recipes.value = await groupRecipeRequest.getRecipesInGroup(groupId.value)
}

const getUsersPermissionsForGroup = async () => {
  const userGroup = await userGroupRequest.getUserGroup(groupId.value, accountStore.currentUserId)
  canAddRecipes.value = userGroup.canAddRecipes
  canAddUsers.value = userGroup.canAddUsers
}

const loadData = async () => {
  await Promise.all([getGroupInfo(), getGroupRecipes(), getUsersPermissionsForGroup()])
}

onMounted(async () => {
  loading.value = true;
  await loadData();
  loading.value = false;
})
</script>

<style scoped lang="scss">
.tab-bar {
  display: flex;
  justify-content: center;
  align-items: center;
  margin-bottom: 24px;
}
.tab-button {
  padding: 10px;
  font-size: 20px;
  height: 32px;
  cursor: pointer;
}
.tab-button:hover {
  font-size: 22px;
}
.active-tab {
  border-bottom: 3px solid #C7D6D5;
}
.group-body {
  display: flex;
  justify-content: space-between;
  flex-direction: row;
  flex-wrap: wrap;
}
.recipe-card {
  border: 1px solid #043565;
  border-radius: 5px;
  flex: 30%;
  cursor: pointer;
  margin: 5px;
}
.recipe-card:hover {
  background-color: #C7D6D5;
  border: 2px solid #C7D6D5;
}
.member-card {
  flex-basis: 90%;

  &__header {
    font-size: 22px;
    font-weight: bold;
  }
}
</style>