import { createRouter, createWebHistory } from 'vue-router'
import AddRecipeOptionsView from "@/views/AddRecipeOptionsView.vue"
import LoginView from "@/views/LoginView.vue"
import RecipeCreationView from "@/views/RecipeCreationView.vue"

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: "/AddRecipeOptions",
      name: "AddRecipeOptionsView",
      component: AddRecipeOptionsView
    },
    {
      path: "/",
      name: "LoginView",
      component: LoginView
    },
    {
      path: "/RecipeCreationView",
      name: "RecipeCreationView",
      component: RecipeCreationView
    }
  ]
})

export default router
