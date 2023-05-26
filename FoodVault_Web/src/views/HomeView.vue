<template>
  <div class="homePageTitle">
    <h2>Welcome Home!</h2>
  </div>
  <div class="homePage">
    <div class="leftDiv">
      <h3>Create a group to share your recipes with!</h3>
      <div>
        <button class="greenButtons" @click="openGroupCreation">
          Create Group
        </button>
        <br /><br />
        <div class="dropDownList" @click="toggleDisplayOfGroups">
          Your Groups &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
          <span v-if="dropDownIsHidden">&dtrif;</span>
          <span v-else> &utrif; </span>
        </div> 
        <br/>
        <div v-if="!dropDownIsHidden" id="dropDown" class="dropdown">
          <div v-for="group in usersGroups" :key="group.id">
            &nbsp;{{ group.name }}
          </div>
        </div>
      </div>
    </div>
    <div class="centerDiv">
      <h3>Share your Recipes with family/friends</h3>
      <div>
        <button class="editButton" @click="openRecipeEditing">
          View/Edit Your Recipes
        </button>
      </div>
    </div>
    <div class="rightDiv">
      <h3>Your Recipes</h3>
      <RouterLink to="/CreateRecipe">
        <button class="greenButtons">Create Recipe</button></RouterLink
      ><br/><br/>
      <div class="dropDownList" @click="toggleDisplayOfRecipes">
        Your Recipes &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <span v-if="recipeDropDownIsHidden">&dtrif;</span>
        <span v-else> &utrif; </span>
      </div>
      <br/>
      <div v-if="!recipeDropDownIsHidden" id="dropDown" class="dropdown">
        <table>
          <div v-for="recipe in usersRecipes" :key="recipe.id">
            <td class="recipeNameColumn"> 
              &nbsp;{{ recipe.name }}
            </td>
            <td class="editColumn">
              <span class="editSpan">
                <button class="editEachRecipe" @click="editRecipe(recipe)">Edit</button>
              </span>
            </td>
          </div>
        </table>
      </div>
    </div>
  </div>
</template>

<script setup>
import { onMounted, ref } from "vue";
import { useRouter } from "vue-router";
import { useAccountStore } from "../stores/accountStore";
import RecipeRequest from "@/requests/recipe-request";
import GroupRequest from "@/requests/group-request";

const router = useRouter();

var dropDownIsHidden = ref(true);
var recipeDropDownIsHidden = ref(true)
const accountStore = useAccountStore();
var usersGroups = ref([]);
var usersRecipes = ref([]);

const openGroupCreation = function () {
  router.push("/CreateGroup");
};

const openRecipeEditing = function () {
  router.push("/EditRecipes");
};

const toggleDisplayOfGroups = function () {
  if (dropDownIsHidden.value == true) {
    dropDownIsHidden.value = false;
  } else {
    dropDownIsHidden.value = true;
  }
};

const toggleDisplayOfRecipes = function () {
  if (recipeDropDownIsHidden.value == true) {
    recipeDropDownIsHidden.value = false;
  } else {
    recipeDropDownIsHidden.value = true;
  }
};

const editRecipe = function (recipe) {
  router.push({
    name: "EditSingleRecipe",
    params: {
      recipeId: recipe.id
    } 
  })
}

onMounted(async () => {
  usersGroups.value = await new GroupRequest().getGroupsOwnedByUser(accountStore.currentUserId);
  usersRecipes.value = await new RecipeRequest().getUserRecipes(accountStore.currentUserId);
});
</script>

<style>
.homePageTitle {
  justify-content: center;
  text-align: center;
}

.homePage {
  display: flex;
  justify-content: space-between;
  flex-direction: row;
  flex-wrap: wrap;
}

.homePage > div {
  flex: 30%;
  min-width: 250px;
  padding: 5px;
  border: 1px solid #c7d6d5;
  min-height: 500px;
  border-radius: 8px;
  margin: 4px 4px 4px 4px;
  justify-content: center;
  text-align: center;
}

.homePage > div:hover {
  border: 3px solid #c7d6d5;
}

.centerDiv {
  background-color: #c7d6d5;
}

.dropdown {
  min-height: 300px;
  max-height: 300px;
  flex-wrap: wrap;
  overflow-y: auto;
  border-top: none;
  border: 3px solid #c7d6d5;
  border-radius: 5px;
  text-align: left;
}

.rightDiv h3 {
  color: #043565;
}

.greenButtons {
  min-height: 30px;
  min-width: 100px;
  background-color: #c7d6d5;
  border-radius: 10px;
  border: 1px solid #043565;
}

.greenButtons:hover {
  box-shadow: 0 4px 4px #c7d6d5;
  cursor: pointer;
}

.editButton {
  min-height: 30px;
  min-width: 100px;
  border-radius: 10px;
  border: 1px solid #043565;
}

.editButton:hover {
  box-shadow: 0 2px 2px lightslategrey;
  cursor: pointer;
}

.dropDownList {
  border: 1px solid #6d7275;
  border-radius: 8px;
  background-color: #c7d6d5;
  width: 100%;
  font-weight: bold;
  color: #043565;
}

.dropDownList:hover {
  cursor: pointer;
  box-shadow: 1px 1px 1px gray;
}

.recipeNameColumn {
  width: 600px;
}
.editColumn {
  width: 5%
}

.editEachRecipe {
  min-height: 20px;
  min-width: 80px;
  background-color: #c7d6d5;
  border-radius: 10px;
  border: 1px solid #043565;
}

.editEachRecipe:hover {
  box-shadow: 0 4px 4px #c7d6d5;
  cursor: pointer;
}

</style>
