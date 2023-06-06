import { createRouter, createWebHistory } from 'vue-router'
import RecipeCreationView from "@/views/RecipeCreationView.vue"
import HomeView from "@/views/HomeView.vue"
import LoginPage from "@/components/LoginPage.vue";
import SignUpPage from "@/components/SignUpPage.vue";
import CreateGroup from "@/components/CreateGroup.vue";
import EditRecipes from "@/components/EditRecipes.vue";
import EditSingleRecipe from "@/components/EditSingleRecipe.vue";
import EditGroup from "@/components/EditGroup.vue";

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: "/",
      name: "Home",
      component: HomeView
    },
    {
      path: "/Login",
      name: "Login",
      component: LoginPage
    },
    {
      path: "/Signup",
      name: "Signup",
      component: SignUpPage
    },
    {
      path: "/CreateGroup",
      name: "CreateGroup",
      component: CreateGroup
    },
    {
      path: "/CreateRecipe",
      name: "CreateRecipe",
      component: RecipeCreationView
    },
    {
      path: "/EditRecipes",
      name: "EditRecipes",
      component: EditRecipes
    },
    {
      path: "/EditSingleRecipe/:recipeId",
      name: "EditSingleRecipe",
      component: EditSingleRecipe,
      props: true
    },
    {
      path: "/EditGroup/:groupId",
      name: "EditGroup",
      component: EditGroup, 
      props: true
    }
  ]
})

export default router
