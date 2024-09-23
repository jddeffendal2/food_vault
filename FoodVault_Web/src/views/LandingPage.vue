<template>
    <div class="homePage">
        <div class="aboutSection">
            <h1 class="aboutSectionHeader"><span class="foodVaultTitle"><span id="firstWord">Food</span><span id="secondWord">Vault</span> </span><br/> <span class="foodVaultSubTitle">A New Way of Sharing Recipes</span></h1>
            <br/>
                <h1 class="aboutParagraph">
                    At Food Vault, our mission is to help friends and family members share their recipes easily and freely with each other through our new innovative app. Food vault allows you to add any recipe you want to your groups, but the benefit is: only people who you invite to your group can see your recipes.
                </h1>
        </div>
        <div class="exampleSection">
            <div class="leftDiv">
                <h1 id="exampleSectionSubTitle">Add Recipes to be Stored on Food Vault</h1><br/>
                <h3 id="exampleSectionText">Create and add as many recipes as you like.</h3><br/>
                <h3 id="exampleSectionText">All recipes you add will be stored privately and only people who you share them with can access them.</h3> <br/>
                <img id="exampleImage" src="../assets/AddRecipesSS.png" alt="Recipe Image" >
                <img id="exampleImage" src="../assets/AllRecipesViewSS.png" alt="Recipe Image" >
                <br/>
                <br/>
                <FvButton @click="addRecipes">Start Adding Recipes Now</FvButton>
            </div>
            <div class="rightDiv">
                <h1 id="exampleSectionSubTitle">Create Groups to Share your Recipes with Anyone You Choose</h1><br/>
                <h3 id="exampleSectionText">Invite users to join your group</h3><br/>
                <h3 id="exampleSectionText">Add recipes to your group and start sharing your recipes with your family and friends now.</h3> <br/>
                <br/><br/>
                <FvButton @click="createGroup">Create a Group</FvButton>
            </div>
        </div>
    </div>

</template>
<script setup>
import FvButton from "@/components/shared/FvButton.vue";
import router from "../router";

const addRecipes = function () {
    router.push('/SignUp');
}
const createGroup = function () {
    router.push('/SignUp');
}
</script>
<style>
.exampleSection {
  display: flex;
  justify-content: space-between;
  flex-direction: row;
  flex-wrap: wrap;
  flex: 46%;
}
.exampleSection > div {
  flex: 46%;
  min-width: 250px;
  padding: 5px;
  border: 2px solid #c7d6d5;
  min-height: 500px;
  border-radius: 8px;
  margin: 4px 4px 4px 4px;
  justify-content: center;
  text-align: center;
}
.exampleSection > div:hover {
    background-color: #c7d6d5;
}
.aboutSection {
    margin-top: 50px;
    margin-bottom: 50px;
    border: 4px solid #c7d6d5;
    border-radius: 12px;
    padding: 5px;
    width: 98%;
    background-color: #c7d6d5;
    display: flex;
    justify-content: space-between;
    flex-wrap: wrap;
}
.aboutParagraph {
    font-size: 28px;
    color: #043565;
    width: 610px;
    display: inline-block;
    margin-left: 50px;
    margin-right: 50px;
}
#exampleImage {
    border-radius: 10px;
    border: 2px solid #c7d6d5;
    width: 355px;
    height: 208px;
}

.foodVaultTitle {
    font-size: 60px;
    margin-left: 50px;
    color: white;
}
.foodVaultSubTitle {
    font-size: 25px;
    margin-left: 50px;
    color:#043565;
}
/* #aboutImage {
    width: 500px;
    display: inline-block;
    padding-left: 40px;
} */
#firstWord {
    color:#043565;
}

#exampleSectionSubTitle {
    margin-left: 50px;
    margin-right: 50px;
    color: #043565;
}
#exampleSectionText {
    color: #043565;
    margin-left: 50px;
    margin-right: 50px;

}
@media screen and (max-width: 780px) {
    #exampleImage {
        width: 80%;
    }
}
</style>