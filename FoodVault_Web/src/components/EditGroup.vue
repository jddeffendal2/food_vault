<template>
  <div class="editGroupDiv">
    <br />
    <h2>Group Name: {{ selectedGroup.name }}</h2>
    <div>{{ selectedGroup.description }}</div>
    <br />
    <div class="viewGroupDiv">
      <div class="sharedUsers">
        Shared with {{ 0 }} Users <button @click="shareGroup" class="greenStyledButton">Share</button>
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
              <th><h2> Recipes in this Group </h2>
                <button @click="showAddRecipeModal" class="greenStyledButton">Add More Recipes</button>
              </th>
            </tr>
            <div
              class="recipeTable"
              v-for="recipe in recipesInGroup"
              :key="recipe"
            >
              <div id="recipeRow" @click="selectRecipe(recipe)">
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
    <ShareGroupFeature v-if="isReadyToShare" @close="closeShareGroupModal"></ShareGroupFeature>
  </div>
</template>

<script setup>
import { ref, onMounted } from "vue";
import GroupRequest from "@/requests/group-request";
import GroupRecipeRequest from "@/requests/group-recipe-request";
import RecipeRequest from "@/requests/recipe-request";
import AddRecipesToGroup from "@/components/AddRecipesToGroup.vue";
import ShareGroupFeature from "@/components/ShareGroupFeature.vue";
import { useRouter } from "vue-router";

const router = useRouter();

var selectedGroup = ref({});
var addedGroupRecipes = ref([]);
var recipesInGroup = ref([]);
var isGroupEmpty = ref(false);
var isReadyToShare = ref(false);

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

const closeAddRecipesModal = async function () {
  isGroupEmpty.value = false;
  addedGroupRecipes.value = await new GroupRecipeRequest().getRecipesInGroup(
    props.groupId
  );
  recipesInGroup.value = []
  for (let i = 0; i < addedGroupRecipes.value.length; i++) {
    recipesInGroup.value.push(
      await new RecipeRequest().getRecipeById(
        addedGroupRecipes.value[i].recipeId
      )
    );
  }
};

const shareGroup = function () {
  isReadyToShare.value = true;
};

const closeShareGroupModal = function () {
  isReadyToShare.value = false;
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
  margin-bottom: 5px;
}

#recipeRow:hover {
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

#recipeTableTitle {
  font-size: 20px;
}

.greenStyledButton {
  color: #043565;
  background: #c7d6d5;
  border: 1px solid #043565;
  border-radius: 2px;
}

.greenStyledButton:hover {
  cursor: pointer;
  box-shadow: 1px 1px 1px gray;
}

</style>
