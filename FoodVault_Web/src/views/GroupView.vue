<template>
  <FvLoadingSpinner v-if="loading" />
  <div class="tab-bar">
    <div
      @click="activeTab = 0"
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
  <div v-if="activeTab === 0" class="group-body">
    <div v-if="recipes.length == 0">
      <br/><br/>
      There are no recipes in this group
    </div>
    <div v-for="recipe in recipes" :key="recipe.id" class="recipe-card" @click="openRecipe(recipe)">
      {{ recipe.name }}
    </div>
  </div>
  <div v-if="activeTab === 1" class="group-body">
    <div class="member-card">
      <div>Owner: {{ owner.firstName }} {{ owner.lastName }}</div>
      <br />
      <div class="member-card__header">Members:</div>
      <table>
        <tr v-for="member in members" :key="member.userId">
          <td>{{ member.firstName }} {{ member.lastName }}</td>
          <td v-if="currentUserIsGroupOwner"><FvButton @click="removeUserFromGroup(member)">Remove</FvButton></td>
        </tr>
      </table>
    </div>
  </div>
  <div v-if="activeTab === 2">
    <EditGroup :group-id="route.params.id" />
  </div>
</template>

<script setup>
import { computed, onMounted, ref } from "vue";
import { useRoute, useRouter } from "vue-router";
import { GroupRequest } from "@/requests/group-request";
import { GroupRecipeRequest } from "@/requests/group-recipe-request";
import { UserGroupRequest } from "@/requests/user-group-request";
import { useAccountStore } from "@/stores/accountStore"
import EditGroup from "@/components/EditGroup.vue";
import FvButton from "@/components/shared/FvButton.vue";
import FvLoadingSpinner from "@/components/shared/FvLoadingSpinner.vue";

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

const currentUserIsGroupOwner = computed(() => {
  return owner.value.userId == accountStore.currentUserId;
})

const openRecipe = function (recipe) {
  router.push("/Recipe/" + recipe.id);
}

const removeUserFromGroup = async (member) => {
  loading.value = true;
  await userGroupRequest.removeUserFromGroup(route.params.id, member.userId);
  await loadData();
  loading.value = false;
}

const loadData = async () => {
  const groupInfo = await groupRequest.getGroupById(route.params.id);
  recipes.value = await groupRecipeRequest.getRecipesInGroup(route.params.id);
  
  owner.value = groupInfo.owner;
  members.value = groupInfo.members;
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
  justify-content: center;
}
.recipe-card {
  border: 1px solid #043565;
  border-radius: 5px;
  flex-basis: 80%;
  cursor: pointer;
}
.member-card {
  flex-basis: 90%;

  &__header {
    font-size: 22px;
    font-weight: bold;
  }
}
</style>