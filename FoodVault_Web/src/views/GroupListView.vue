<template>
  <h2>Your Groups</h2>
  <div v-for="group in groupsOwned" :key="group.id" class="group-row">
    <div @click="openGroup(group.groupId)">{{ group.name }}</div>
  </div>
  <h2>Groups You Are A Member Of</h2>
  <div v-for="group in groupsMember" :key="group.id">
    <div @click="openGroup(group.groupId)">{{ group.name }}</div>
  </div>
</template>

<script setup>
import { onMounted, ref } from "vue";
import { useAccountStore } from "@/stores/accountStore";
import { useRouter } from "vue-router";
import GroupRequest from "@/requests/group-request";

const router = useRouter();
const accountStore = useAccountStore();
const groupRequest = new GroupRequest();

const groupsOwned = ref([]);
const groupsMember = ref([]);

const openGroup = function (id) {
  router.push(`/Group/${id}`)
}

onMounted(async () => {
  groupsOwned.value = await groupRequest.getGroupsOwnedByUser(accountStore.currentUserId);
  groupsMember.value = await groupRequest.getGroupsWhereUserIsMember(accountStore.currentUserId);
})
</script>

<style>
.group-row {
  cursor: pointer;
}
</style>