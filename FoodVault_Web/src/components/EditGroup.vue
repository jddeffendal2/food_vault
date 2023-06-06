<template>
  <div class="editGroupDiv">
    <br />
    <h2>Group Name: {{ selectedGroup.name }}</h2>
    <div>{{ selectedGroup.description }}</div>
    <br />
    <div class="viewGroupDiv">
      <div class="sharedUsers">
        Shared with {{ 0 }} Users <button>Share</button>
      </div>
      <div class="addedRecipes">
        <p v-if="addedGroupRecipes.length == 0">
          This group contains no recipes.
          <button @click="showAddRecipeModal">Add Recipes</button>
        </p>
        <div v-else class="recipeTableDiv">
          <table>
            <br /><br />
            <tr>
              <th><h2>Recipes in this Group</h2></th>
            </tr>
            <div
              class="recipeTable"
              v-for="recipe in recipesInGroup"
              :key="recipe"
            >
              <div id="recipeRow">
                <td>{{ recipe.name }}</td>
                <td>{{ recipe.description }}</td>
              </div>
            </div>
          </table>
        </div>
      </div>
    </div>
    <AddRecipesToGroup
      v-if="isGroupEmpty"
      :selectedGroup="selectedGroup"
      @close="closeAddRecipesModal"
    ></AddRecipesToGroup>
  </div>
</template>

<script setup>
import { ref, onMounted } from "vue";
import GroupRequest from "@/requests/group-request";
import GroupRecipeRequest from "@/requests/group-recipe-request";
import RecipeRequest from "@/requests/recipe-request";
import AddRecipesToGroup from "@/components/AddRecipesToGroup.vue";

var selectedGroup = ref({});
var addedGroupRecipes = ref([]);
var recipesInGroup = ref([]);
var isGroupEmpty = ref(false);

const props = defineProps({
  groupId: {
    type: String,
    required: true,
  },
});

onMounted(async () => {
  selectedGroup.value = await new GroupRequest().getGroupById(props.groupId);
  addedGroupRecipes.value = await new GroupRecipeRequest().getRecipesInGroup(
    props.groupId
  );
  for (let i = 0; i < addedGroupRecipes.value.length; i++) {
    recipesInGroup.value.push(
      await new RecipeRequest().getRecipeById(
        addedGroupRecipes.value[i].recipeId
      )
    );
  }
});

const showAddRecipeModal = function () {
  isGroupEmpty.value = true;
};

const closeAddRecipesModal = function () {
  isGroupEmpty.value = false;
};
</script>

<style scoped>
.editGroupDiv {
  justify-content: center;
  text-align: center;
}

.recipeTableDiv {
  min-width: 500px;
  max-width: 500px;
}
.recipeTableDiv,
th {
  margin-left: auto;
  margin-right: auto;
}
th, #recipeRow {
  border: 2px solid #c7d6d5;
  text-align: center;
}

th {
  width: 500px;
}
td {
  width: 250px;
}
</style>
