<template>
  <div class="recipe">
    <div class="recipe__image">
      <br/><br/>
      <img src="../assets/mac-and-cheese.jpg" alt="Recipe Image" width="350" height="350"/>
    </div>
    <div class="recipe__info">
      <h3 class="recipe__title">{{ recipe.name }}</h3>
      <div class="recipe__description">{{ recipe.description }} </div>
      <h3 class="recipe__ingredients">Ingredients</h3>
      <div
        v-for="ingredient in ingredients"
        :key="ingredient.ingredientId"
      >
        {{ ingredient.name }} - {{ ingredient.quantity }} {{ ingredient.unitOfMeasurement }}
      </div>
    </div>
    <div class="recipe__button">
      <FvButton @click="editRecipe">Edit Recipe</FvButton>
  <div class="uninvitedUser" v-if="isUserUnauthorized">
    <UnauthorizedRecipeViewerModal></UnauthorizedRecipeViewerModal>
  </div>
  <div>
    <div class="recipe-div">
      <div class="left-picture">
        <br /><br />
        <img
          src="../assets/mac-and-cheese.jpg"
          alt="Recipe Picture"
          width="350"
          height="350"
        />
      </div>
      <div class="right-info">
        <h3 class="recipe-title">{{ recipe.name }}</h3>
        <div id="recipe-description-div">{{ recipe.description }}</div>
        <h3 id="recipe-ingredients">Ingredients</h3>
        {{ recipe.ingredients }}
      </div>
      <div class="button-div">
        <FvButton @click="editRecipe">Edit Recipe</FvButton>
      </div>
      <br /><br />
    </div>
    <div class="bottom-div">
      <h3>Instructions</h3>
    </div>
  </div>
  <div class="bottom-div">
    <h3>Instructions</h3>
  </div>
</template>

<script setup>
import { onMounted, ref } from "vue";
import { useAccountStore } from "@/stores/accountStore";
import { useRoute } from "vue-router";
import { useRouter } from "vue-router";
import RecipeRequest from "@/requests/recipe-request";
import RecipeIngredientRequest from "@/requests/recipe-ingredient-request";
import GroupRecipeRequest from "@/requests/group-recipe-request";
import UserGroupRequest from "@/requests/user-group-request";
import FvButton from "@/components/shared/FvButton.vue";
import UnauthorizedRecipeViewerModal from "@/components/UnauthorizedRecipeViewerModal.vue";

const route = useRoute();
const router = useRouter();

const accountStore = useAccountStore();
const recipeRequest = new RecipeRequest();
const recipeIngredientRequest = new RecipeIngredientRequest();
const groupRecipeRequest = new GroupRecipeRequest();
const userGroupRequest = new UserGroupRequest();
const groupsThatContainSelectedRecipe = ref([]);
const usersInGroups = ref([]);
const canUserSeeRecipe = ref(false);
const isUserUnauthorized = ref(false);

const recipe = ref({});
const ingredients = ref([]);

const editRecipe = function () {
  router.push({
    name: "EditSingleRecipe",
    params: {
      recipeId: recipe.value.id,
    },
  });
};
onMounted(async () => {
  recipe.value = await recipeRequest.getRecipeById(route.params.id);
  ingredients.value = await recipeIngredientRequest.getAllIngredientsForRecipe(route.params.id);
  console.log(recipes.value)
})
  groupsThatContainSelectedRecipe.value = await groupRecipeRequest.getGroupsThatContainRecipe(route.params.id);
  for (let i = 0; i<groupsThatContainSelectedRecipe.value.length; i++) {
    usersInGroups.value.push(await userGroupRequest.getAllUsersInSpecificGroups(groupsThatContainSelectedRecipe.value[i].groupId));
  }
  for (let i=0; i<usersInGroups.value.length; i++) {
    if (usersInGroups.value[i].includes(accountStore.currentUserId)) {
      canUserSeeRecipe.value = true;
    }
  }
  if (recipe.value.userId == accountStore.currentUserId || canUserSeeRecipe.value) {
    console.log("You are allowed to View This Recipe")
  }
  else{
    isUserUnauthorized.value = true;
  }
});
</script>

<style scoped lang="scss">
.recipe {
  text-align: center;
  display: flex;
  justify-content: space-between;
  flex-direction: row;
  flex-wrap: wrap;

  &__image {
    flex: 30%;
    min-width: 400px;
    max-width: 400px;
    padding: 5px;
    min-height: 400px;
    border-radius: 8px;
    margin: 10px 0px 0px 150px;
    justify-content: center;
    text-align: center;
    /* border: 1px solid #043565; */
  }

  &__info {
    flex: 30%;
    min-width: 500px;
    max-width: 500px;
    padding: 5px;
    min-height: 400px;
    border-radius: 8px;
    margin: 20px 4px 4px 4px;
    justify-content: center;
    text-align: center;
    /* border: 1px solid #043565; */
  }
  
  &__title {
    font-size: 35px;
    color: #043565;
    font-weight: bold;
    text-decoration: overline;
  }

  &__description {
    min-width: 400px;
    max-width: 400px;
    margin: auto auto;
    text-align: center;
  }

  &__ingredients {
    color: #043565;
  }
}

.button-div {
  flex: 30%;
  min-width: 100px;
  max-width: 100px;
  padding: 5px;
  min-height: 100px;
  border-radius: 8px;
  margin: 50px 4px 4px 4px;
  justify-content: center;
  text-align: center;
  /* border: 1px solid #043565; */
}

.bottom-div {
  /* border: 1px solid #043565; */
  color: #043565;
  margin: 4px 136px 4px 150px;
  text-align: center;
}
</style>
