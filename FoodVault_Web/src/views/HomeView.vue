<template>
  <div class="homePageTitle">
    <h2>Welcome Home!</h2>
  </div>
  <div class="homePage">
    <div class="leftDiv">
      <h3>Create a group to share your recipes with!</h3>
      <div>
        <button @click="openGroupCreation">Create Group</button>
      </div>
    </div>
    <div class="centerDiv">
      <h3>Share your Recipes with family/friends</h3>
      <div>
        <button @click="openRecipeEditing">View/Edit Your Recipes</button>
      </div>
    </div>
    <div class="rightDiv">
      <h3>Create your first Recipe: <RouterLink to="/AddRecipeOptions"> <button class="createRecipe">Create
            Recipe</button></RouterLink>
      </h3>
    </div>
  </div>
</template>

<script setup>
import { useRouter } from "vue-router";

const router = useRouter();
const openGroupCreation = function() {
  router.push("/CreateGroup");
}

const openRecipeEditing = function() {
  router.push("/EditRecipes");
}

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
  border: 1px solid #C7D6D5;
  min-height: 500px;
  border-radius: 8px;
  margin: 4px 4px 4px 4px;
  justify-content: center;
  text-align: center;
}

.homePage > div:hover {
  border: 3px solid #C7D6D5;
}

.centerDiv {
  background-color: #C7D6D5;
}

.rightDiv h3 {
  color: #043565;
}

.createRecipe {
  min-height: 30px;
  min-width: 100px;
  background-color: #C7D6D5;
  border-radius: 10px;
  border: 1px solid #043565;
}

.createRecipe:hover {
  box-shadow: 0 4px 4px #C7D6D5;
  cursor: pointer;
}</style>