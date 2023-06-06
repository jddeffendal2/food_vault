<template>
  <div class="editGroupDiv">
    <br/>
    <h2> Group Name: {{ selectedGroup.name }} </h2>
    <div> {{ selectedGroup.description }} </div>
    <br/>
    <div class="viewGroupDiv">
      <div class="sharedUsers">
        Shared with {{ 0 }} Users <button>Share</button>
      </div>
      <div class="addedRecipes">
        <p v-if="addedGroups.length == 0">This group contains no recipes. <button @click="showAddRecipeModal">Add Recipes</button></p>
        <p v-else v-for="recipe in addedGroups" :key="recipe"> {{ recipe }}</p>
      </div>
    </div>
    <AddRecipesToGroup v-if="isGroupEmpty" :selectedGroup="selectedGroup" @close="closeAddRecipesModal"></AddRecipesToGroup>
  </div>
</template>

<script setup>
import { ref, onMounted } from "vue";
import GroupRequest from "@/requests/group-request";
import GroupRecipeRequest from "@/requests/group-recipe-request";
import AddRecipesToGroup from "@/components/AddRecipesToGroup.vue";

var selectedGroup = ref({});
var addedGroups = ref([]);
var isGroupEmpty = ref(false);

const props = defineProps({
  groupId: {
    type: String,
    required: true,
  },
});

onMounted(async () => {
  selectedGroup.value = await new GroupRequest().getGroupById(props.groupId);
  addedGroups.value = await new GroupRecipeRequest().getRecipesInGroup(props.groupId);
});

const showAddRecipeModal = function () {
  isGroupEmpty.value = true;
}

const closeAddRecipesModal = function () {
  isGroupEmpty.value = false;
}
</script>

<style scoped>
.editGroupDiv{
  justify-content: center;
  text-align: center;
}
</style>
