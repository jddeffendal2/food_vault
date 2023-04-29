<template>
  <h2>Create a Group</h2>
  <div>
    Group Name:
    <input v-model="name" />
  </div>
  <div>
    Description:
    <input v-model="description" />
  </div>
  <button @click="createGroup">Create Group</button>
</template>

<script setup>
import { ref } from "vue";
import { useAccountStore } from "../stores/accountStore";
import { saveGroup } from "../utils/request.js";

const accountStore = useAccountStore();

var name = ref("");
var description = ref("");

const createGroup = function () {
  const group = {
    name: name.value,
    description: description.value,
    creator: accountStore.currentUserId
  };
  saveGroup(group, (res) => console.log(res));
}
</script>

<style scoped>
</style>