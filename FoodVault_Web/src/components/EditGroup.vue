<template>
  <div class="edit-group-div">
    <br />
    <h2>{{ selectedGroup.name }}</h2>
    <div>{{ selectedGroup.description }}</div>
    <br />
    <div class="view-group-div">
      <div class="shared-users">
        Shared with {{ sharedUsers.length }} Users <button @click="isReadyToShare = true" class="greenStyledButton">Share</button>
      </div>
      <div class="added-recipes">
        <p v-if="addedGroupRecipes.length == 0">
          This group contains no recipes.
          <button @click="isGroupEmpty = true">Add Recipes</button>
        </p>
        <div v-else class="recipe-table-div">
          <table>
            <br /><br />
            <tr>
              <th>
                <h2> Recipes in this Group </h2>
                <FvButton @click="isGroupEmpty = true">Add More Recipes</FvButton>
              </th>
            </tr>
            <div class="recipe-table" v-for="recipe in recipesInGroup" :key="recipe">
              <div id="recipe-row" @click="selectRecipe(recipe)">
                <td>{{ recipe.name }}</td>
                <td>{{ recipe.description }}</td>
              </div>
            </div>
          </table>
        </div>
      </div>
    </div>
    <AddRecipesToGroup v-if="isGroupEmpty" :selected-group="selectedGroup" @close="closeAddRecipesModal" />
    <ShareGroupFeature v-if="isReadyToShare" :group-id="groupId" :shared-users="sharedUsersIds" @close="isReadyToShare = false" />
  </div>
</template>

<script setup>
import { ref, onMounted } from "vue";
import { GroupRequest } from "@/requests/group-request";
import { GroupRecipeRequest } from "@/requests/group-recipe-request";
import { RecipeRequest } from "@/requests/recipe-request";
import { UserGroupRequest } from "@/requests/user-group-request";
import AddRecipesToGroup from "@/components/AddRecipesToGroup.vue";
import ShareGroupFeature from "@/components/ShareGroupFeature.vue";
import { useRouter } from "vue-router";
import FvButton from "@/components/shared/FvButton.vue";

const router = useRouter();

const groupRequest = new GroupRequest();
const groupRecipeRequest = new GroupRecipeRequest();
const recipeRequest = new RecipeRequest();
const userGroupRequest = new UserGroupRequest();

const selectedGroup = ref({});
const addedGroupRecipes = ref([]);
const recipesInGroup = ref([]);
const isGroupEmpty = ref(false);
const isReadyToShare = ref(false);
const allUserGroups = ref([]);
const sharedUsers = ref([]);
const sharedUsersIds = ref([]);

const props = defineProps({
  groupId: {
    type: String,
    required: true,
  },
});

onMounted(async () => {
  selectedGroup.value = await groupRequest.getGroupById(props.groupId)
  addedGroupRecipes.value = await groupRecipeRequest.getRecipesInGroup(
    props.groupId
  );
  for (let i = 0; i < addedGroupRecipes.value.length; i++) {
    recipesInGroup.value.push(
      await recipeRequest.getRecipeById(
        addedGroupRecipes.value[i].id
      )
    );
  }

  allUserGroups.value = await userGroupRequest.getAllUserGroups();
  for (let i = 0; i < allUserGroups.value.length; i++) {
    if (allUserGroups.value[i].groupId == props.groupId) {
      sharedUsers.value.push(allUserGroups.value[i]);
      sharedUsersIds.value.push(allUserGroups.value[i].userId);
    }
  }
});

const closeAddRecipesModal = async function () {
  isGroupEmpty.value = false;
  addedGroupRecipes.value = await groupRecipeRequest.getRecipesInGroup(
    props.groupId
  );
  recipesInGroup.value = []
  for (let i = 0; i < addedGroupRecipes.value.length; i++) {
    recipesInGroup.value.push(
      await recipeRequest.getRecipeById(
        addedGroupRecipes.value[i].id
      )
    );
  }
};

const selectRecipe = function (recipe) {
  router.push({
    name: "EditSingleRecipe",
    params: {
      recipeId: recipe.id
    }
  });
}
</script>

<style scoped>
.edit-group-div {
  justify-content: center;
  text-align: center;
}

.recipe-table-div {
  min-width: 500px;
  max-width: 500px;
}

.recipe-table-div,
th {
  margin-left: auto;
  margin-right: auto;
}

th,
#recipe-row {
  border: 2px solid #c7d6d5;
  text-align: center;
  margin-bottom: 5px;
}

#recipe-row:hover {
  box-shadow: 0px 0px 3px #043565;
  cursor: pointer;
  background-color: #c7d6d5;
  border: 2px solid #c7d6d5;
}

th {
  width: 500px;
}

td {
  width: 250px;
}

#recipe-table-title {
  font-size: 20px;
}

.green-styled-button {
  color: #043565;
  background: #c7d6d5;
  border: 1px solid #043565;
  border-radius: 2px;
}

.green-styled-button:hover {
  cursor: pointer;
  box-shadow: 1px 1px 1px gray;
}
</style>
