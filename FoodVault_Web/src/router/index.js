import { createRouter, createWebHistory } from 'vue-router'
import AddRecipeOptionsView from "@/views/AddRecipeOptionsView.vue"
import HomeView from "@/views/HomeView.vue"
import RecipeCreationView from "@/views/RecipeCreationView.vue"
import LoginPage from "@/components/LoginPage.vue";
import SignUpPage from "@/components/SignUpPage.vue";
import CreateGroup from "@/components/CreateGroup.vue";

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
      path: "/AddRecipeOptions",
      name: "AddRecipeOptionsView",
      component: AddRecipeOptionsView
    },
    {
      path: "/RecipeCreationView",
      name: "RecipeCreationView",
      component: RecipeCreationView
    }
  ]
})

export default router
