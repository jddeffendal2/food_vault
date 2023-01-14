<script >
export default {
    name: 'AddRecipeOptionsView',
    mounted(){

    },
    methods: {
        saveRecipe() {

        }
    }
}
</script>

<template>
  <div class="div-main">
    <h2>Create/Add Your Own Recipes To Food Vault</h2>
    <br/>
    <label for="recipeName">Name of Recipe: </label>
    <input type="text" id="recipeName" v-model="recipeName" />
    <br/><br/>
    <div>
        <router-link to="/">Upload Recipe Image/File</router-link> |
        <router-link to="/ManualRecipeCreation">Manually Add Recipe</router-link>
    </div>
    
    <br/><br/>
    <button type="submit" id="saveRecipe" @click="saveRecipe">Save Recipe</button>
  </div>
</template>

<style scoped>
.div-main {
  text-align: center;
}
</style>