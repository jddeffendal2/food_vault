<template>
  <h3 class="groupListTitle">View Groups </h3>
  <select v-model="viewType" class="groupTypeSelection">
    <option value="0">All Groups</option>
    <option value="1">Groups You Created</option>
    <option value="2">Groups You're a Member Of</option>
  </select>
  <div class="groups">
    <div
      v-for="group in groupsToDisplay"
      :key="group.groupId"
      class="group"
      @click="openGroup(group.id)"
    >
      <div class="group__name">{{ group.name }}</div>
      <div>{{ group.description }}</div>
      <div>Members: {{ group.userCount }}</div>
      <div>Recipes: {{ group.recipeCount }}</div>
    </div>
  </div>
</template>

<script setup>
import { computed, onMounted, ref } from "vue";
import { useAccountStore } from "@/stores/accountStore";
import { useRouter } from "vue-router";
import { GroupRequest } from "@/requests/group-request";

const router = useRouter();
const accountStore = useAccountStore();
const groupRequest = new GroupRequest();

const viewType = ref("0")
const allGroups = ref([])

const groupsToDisplay = computed(() => {
  if (viewType.value === "0") {
    return allGroups.value;
  } else if (viewType.value === "1") {
    return allGroups.value.filter(x => x.isOwner)
  } else {
    return allGroups.value.filter(x => !x.isOwner)
  }
})

const openGroup = function (id) {
  router.push(`/Group/${id}`)
}

onMounted(async () => {
  allGroups.value = await groupRequest.getAllGroupsForUser(accountStore.currentUserId);
})
</script>

<style>
.groups {
  margin: 20px;
}
.group {
  cursor: pointer;
  margin-bottom: 20px;
}
.group:hover {
  padding-left: 4px;
  border-left: 4px solid #043565;
}
.group__name {
  font-weight: bold;
}
.groupTypeSelection {
  border: 2px solid #043565;
  border-radius: 5px;
  width: 300px;
  height: 25px;
  margin-left: 20px;
}
.groupListTitle {
  text-align: center;
  font-size: 30px;
  color: #043565;
}
</style>