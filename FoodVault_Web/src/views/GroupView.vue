<template>
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
  </div>
  <div v-if="activeTab === 0">Recipes body</div>
  <div v-else>Members body</div>
</template>

<script setup>
import { onMounted, ref } from "vue";
import { useRoute } from "vue-router";
import GroupRequest from "@/requests/group-request";
import GroupRecipeRequest from "@/requests/group-recipe-request";

const route = useRoute();
const groupRequest = new GroupRequest();
const groupRecipeRequest = new GroupRecipeRequest();

const activeTab = ref(0);

const owner = ref({});
const members = ref([]);
const recipes = ref([]);
//background-color: #C7D6D5;

onMounted(async () => {
  const groupInfo = await groupRequest.getGroupById(route.params.id);
  owner.value = groupInfo.owner;
  members.value = groupInfo.members;

  const temp = await groupRecipeRequest.getRecipesInGroup(route.params.id);
})
</script>

<style>
.tab-bar {
  display: flex;
  justify-content: center;
  align-items: center;
}
.tab-button {
  padding: 10px;
  font-size: 14px;
  height: 25px;
  cursor: pointer;
}
.tab-button:hover {
  font-size: 16px;
}
.active-tab {
  border-bottom: 3px solid #C7D6D5;
}
</style>