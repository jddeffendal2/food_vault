<template>
  <div class="modal-background">
    <div class="modal">
      <header class="modal-header">
        <slot id="header" name="header">
          Add Recipes to Group: {{ selectedGroup.name }}
        </slot>
        <button type="button" class="btn-close" @click="close">x</button>
      </header>

      <section class="modal-body">
        <div class="recipe-view" v-for="recipe in recipesNotInGroup" :key="recipe">
          <td>Picture</td>
          <td class="recipe-name-column">{{ recipe.name }}</td>
          <td>
            <span>
              <button v-if="!computedAddedRecipes.includes(recipe.id)" class="add-recipe" @click="addRecipe(recipe)">
                Add
              </button>
              <button v-else class="add-recipe" @click="removeRecipe(recipe)">
                Remove
              </button>
            </span>
          </td>
        </div>
        <br /><br />
        <button type="button" class="close-button" @click="close">
          Save Recipes
        </button>
      </section>
    </div>
  </div>
</template>
<script setup>
import { ref, onMounted, computed } from "vue";
import { useAccountStore } from "@/stores/accountStore";
import { RecipeRequest } from "@/requests/recipe-request";
import { GroupRecipeRequest } from "@/requests/group-recipe-request";

const accountStore = useAccountStore()

const groupRecipeRequest = new GroupRecipeRequest()

const usersRecipes = ref([]);
const recipesAlreadyInGroup = ref([])
var recipesNotInGroup = ref([]);
var isRecipeAlreadyInGroup = ref(false);
const addedRecipes = ref([])

const props = defineProps({
  selectedGroup: {
    type: Object,
    required: true
  }
})

onMounted(async () => {
  usersRecipes.value = await new RecipeRequest().getUserRecipes(accountStore.currentUserId);
  recipesAlreadyInGroup.value = await groupRecipeRequest.getRecipesInGroup(props.selectedGroup.groupId);  
  for (let i = 0; i < usersRecipes.value.length; i++) {
    isRecipeAlreadyInGroup.value = false;

    for (let j = 0; j < recipesAlreadyInGroup.value.length; j++) {
      if (usersRecipes.value[i].id == recipesAlreadyInGroup.value[j].id) {
        isRecipeAlreadyInGroup.value = true;
      }
    }
    if (!isRecipeAlreadyInGroup.value) {
      recipesNotInGroup.value.push(usersRecipes.value[i])
    }
  }
});

const emit = defineEmits(["close"]);

const close = async function () {
  for (let i = 0; i < addedRecipes.value.length; i++) {
    const groupRecipe = {
      groupId: props.selectedGroup.groupId,
      recipeId: addedRecipes.value[i]
    };
    await groupRecipeRequest.createGroupRecipe(groupRecipe);
  }
  emit("close");
};

const addRecipe = function (recipe) {
  addedRecipes.value.push(recipe.id)
};

const removeRecipe = function (recipe) {
  var updatedAddedRecipes = []
  for (let i = 0; i < addedRecipes.value.length; i++) {
    if (addedRecipes.value[i] != recipe.id) {
      updatedAddedRecipes.push(addedRecipes.value[i])
    }
  }
  addedRecipes.value = updatedAddedRecipes;
}

const computedAddedRecipes = computed(() => {
  return addedRecipes.value;
}
)
</script>
<style scoped>
.modal-background {
  position: fixed;
  top: 0;
  bottom: 0;
  left: 0;
  right: 0;
  background-color: rgba(0, 0, 0, 0.3);
  display: flex;
  justify-content: center;
  align-items: center;
}

.modal {
  min-width: 550px;
  max-width: 550px;
  min-height: 400px;
  max-height: 400px;
  background: #ffffff;
  overflow-x: auto;
  display: flex;
  flex-direction: column;
  border-radius: 15px;
}
.modal-header {
  border-radius: 15px;
  padding: 15px;
  display: flex;
  background-color: #c7d6d5;
  position: relative;
  border-bottom: 1px solid #eeeeee;
  color: #043565;
  font-weight: bold;
  justify-content: center;
}

.modal-body {
  position: relative;
  padding: 20px 10px;
}

.btn-close {
  position: absolute;
  top: 0;
  right: 0;
  border: none;
  font-size: 20px;
  padding: 10px;
  cursor: pointer;
  font-weight: bold;
  color: #043565;
  background: transparent;
}

.close-button {
  color: #043565;
  background: #c7d6d5;
  border: 1px solid #4aae9b;
  border-radius: 2px;
}

.close-button:hover {
  cursor: pointer;
  box-shadow: 1px 1px 1px gray;
}

.recipe-view {
  border: 1px solid #c7d6d5;
  min-height: 50px;
  max-height: 50px;
}

.add-recipe {
  min-height: 20px;
  min-width: 80px;
  background-color: #c7d6d5;
  border-radius: 10px;
  border: 1px solid #043565;
}

.add-recipe:hover {
  box-shadow: 0 4px 4px #c7d6d5;
  cursor: pointer;
}

.recipe-name-column {
  width: 600px;
}
</style>
