<template>
  <div class="editGroupDiv">
    <br/>
    <h2> Group Name: {{ selectedGroup.name }} </h2>
    <div class="viewGroupDiv">
      <div class="sharedUsers">
        
      </div>
      <div class="addedRecipes">

      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted } from "vue";
import GroupRequest from "@/requests/group-request";

var selectedGroup = ref({})

const props = defineProps({
  groupId: {
    type: String,
    required: true,
  },
});

onMounted(async () => {
  selectedGroup.value = await new GroupRequest().getGroupById(props.groupId);
});
</script>

<style scoped>
.editGroupDiv{
  justify-content: center;
  text-align: center;
}
</style>
