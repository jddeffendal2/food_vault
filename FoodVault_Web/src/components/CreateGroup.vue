<template>
  <br/><br/>
  <div class="createGroup">
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
    <button class="createGroupButton" @click="createGroup">Create Group</button>
    <br/><br/>
  </div>
</template>

<script setup>
import { ref } from "vue";
import { useAccountStore } from "../stores/accountStore";
import { useRouter } from "vue-router";
import GroupRequest from "@/requests/group-request";

const router = useRouter();
const accountStore = useAccountStore();

var name = ref("");
var description = ref("");

const createGroup = async function () {
  const group = {
    name: name.value,
    description: description.value,
    creator: accountStore.currentUserId
  };
  await new GroupRequest().createGroup(group);
  router.push("/");
}
</script>

<style scoped>
.createGroup {
  text-align: center;
  color: #043565;
  font-weight: bold;
  background-color: #c7d6d5;
  border: 2px solid #6D7275;
  border-radius: 4px;
  margin-left: auto;
  margin-right: auto;
  max-width: 500px;
  min-width: 500px;
  min-height: 250px;
  max-height: 250px;
}

.createGroupButton {
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

.createGroupButton:hover {
  box-shadow: 1px 1px 1px gray;
  cursor: pointer;
}
</style>