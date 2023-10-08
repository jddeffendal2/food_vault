import { createRouter, createWebHistory } from 'vue-router'
import RecipeCreationView from "@/views/RecipeCreationView.vue"
import HomeView from "@/views/HomeView.vue"
import LoginPage from "@/components/LoginPage.vue";
import SignUpPage from "@/components/SignUpPage.vue";
import CreateGroup from "@/components/CreateGroup.vue";
import EditSingleRecipe from "@/components/EditSingleRecipe.vue";
import EditGroup from "@/components/EditGroup.vue";
import GroupListView from "@/views/GroupListView.vue";
import GroupView from "@/views/GroupView.vue";
import RecipeView from "@/views/RecipeView.vue";
import RecipeListView from "@/views/RecipeListView.vue";
import InvitationView from "@/views/InvitationView.vue";

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
      path: "/Recipes",
      name: "Recipes",
      component: RecipeListView
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
    },
    {
      path: "/Groups", // group list
      name: "GroupsView",
      component: GroupListView
    },
    {
      path: "/Group/:id", // specific group view
      name: "GroupView",
      component: GroupView
    },
    {
      path: "/Recipe/:id",
      name: "RecipeView",
      component: RecipeView
    },
    {
      path: "/Invitations",
      name: "InvitationView", 
      component: InvitationView
    }
  ]
})

export default router
