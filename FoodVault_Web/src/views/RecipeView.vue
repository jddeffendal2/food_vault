<template>
  <div class="recipe-div">
    <div class="left-picture">
      <br/><br/>
      <img src="../assets/mac-and-cheese.jpg" alt="Recipe Picture" width="350" height="350"/>
    </div>
    <div class="right-info">
      <h3 class="recipe-title">{{ recipe.name }}</h3>
      <div id="recipe-description-div">{{ recipe.description }} </div>
      <h3 id="recipe-ingredients">Ingredients</h3>
      <!-- <div> -->
        {{ recipe.ingredients }}
      <!-- </div> -->
    </div>
    <div class="button-div">
      <FvButton @click="editRecipe">Edit Recipe</FvButton>
    </div>
    <br/><br/>
  </div>
  <div class="bottom-div">
    <h3>Instructions</h3>
  </div>
  <!-- <FvButton @click="editRecipe">Edit</FvButton> -->
</template>

<script setup>
import { onMounted, ref } from "vue";
import { useRoute } from "vue-router";
import { useRouter } from "vue-router";
import RecipeRequest from "@/requests/recipe-request";
import FvButton from "@/components/shared/FvButton.vue";


const route = useRoute();
const router = useRouter();

const recipeRequest = new RecipeRequest();

const recipe = ref({});

const editRecipe = function() {
  router.push({
    name: "EditSingleRecipe",
    params: {
      recipeId: recipe.value.id
    }
  });
}
onMounted(async () => {
  recipe.value = await recipeRequest.getRecipeById(route.params.id);
})
</script>

<style scoped>
.recipe-div {
  text-align: center;
  display: flex;
  justify-content: space-between;
  flex-direction: row;
  flex-wrap: wrap;
}
.left-picture {
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

.right-info {
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

.recipe-title {
  font-size: 35px;
  color: #043565;
  font-weight: bold;
  text-decoration: overline;
}

#recipe-description-div {
  min-width: 400px;
  max-width: 400px;
  margin: auto auto;
  text-align: center;
}
#recipe-ingredients {
  color: #043565;
}
.bottom-div {
  /* border: 1px solid #043565; */
  color: #043565;
  margin: 4px 136px 4px 150px;
  text-align: center;

}
</style>
