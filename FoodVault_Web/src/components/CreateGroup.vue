<template>
  <br/><br/>
  <div class="create-group">
    <h2>Create a Group</h2>
    <div>
      Group Name:
      <input v-model="name" />
    </div>
    <br/>
    <div>
      Description:
      <input v-model="description" />
    </div>
    <br/><br/>
    <button class="create-group-button" @click="createGroup">Create Group</button>
    <br/><br/>
  </div>
</template>

<script setup>
import { ref } from "vue";
import { useAccountStore } from "@/stores/accountStore";
import { useRouter } from "vue-router";
import { GroupRequest } from "@/requests/group-request";

const router = useRouter();
const accountStore = useAccountStore();

var name = ref("");
var description = ref("");

const createGroup = async function () {
  const group = {
    name: name.value,
    description: description.value,
    ownerId: accountStore.currentUserId
  };
  await new GroupRequest().createGroup(group);
  router.push("/");
}
</script>

<style scoped>
.create-group {
  text-align: center;
  color: #043565;
  font-weight: bold;
  background-color: #c7d6d5;
  border: 2px solid #043565;
  border-radius: 4px;
  margin-left: auto;
  margin-right: auto;
  max-width: 500px;
  min-width: 500px;
  min-height: 250px;
  max-height: 250px;
}

.create-group-button {
  min-width: 100px;
  max-width: 100px;
  min-height: 40px;
  max-height: 40px;
  border-radius: 8px;
  border: 1px solid #043565;
  color: #043565;
  font-weight: bold;
  font-family: 'Quicksand', sans-serif;
}

.create-group-button:hover {
  box-shadow: 1px 1px 1px lightgray;
  background-color: gainsboro;
  cursor: pointer;
}
</style>