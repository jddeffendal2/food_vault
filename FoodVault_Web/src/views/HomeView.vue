<template>
  <div class="home-page-title">
    <h2>Welcome Home!</h2>
  </div>
  <div class="home-page">
    <div class="left-div">
      <h3>Create a group to share your recipes with!</h3>
      <div>
        <button class="green-buttons" @click="openGroupCreation">
          Create Group
        </button>
        <br /><br />
        <div class="drop-down-list" @click="groupsHidden = !groupsHidden">
          Your Groups &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
          <span v-if="groupsHidden">&dtrif;</span>
          <span v-else> &utrif; </span>
        </div>
        <br />
        <div v-if="!groupsHidden && (usersGroups.length > 0)" id="groups-drop-down" class="drop-down">
          <div v-for="group in usersGroups" :key="group.id" @click="editGroup(group)">
            <td class="name-column">&nbsp;{{ group.name }}</td>
            <td class="edit-column">
              <span class="edit-span">
                <button class="edit-each-row">
                  View/Edit
                </button>
              </span>
            </td>
          </div>
        </div>
        <div v-if="!groupsHidden && (usersGroups.length == 0)">
          <h4 class="no-groups-created">You have no groups. <router-link to="/CreateGroup">Create Group?</router-link></h4>
        </div>
        <div class="drop-down-list" @click="invitationsHidden = !invitationsHidden">
          Invitations &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
          <span v-if="invitationsHidden">&dtrif;</span>
          <span v-else> &utrif; </span>
        </div>
        <div v-if="!invitationsHidden && invitations.length > 0" id="invitations-drop-down" class="drop-down">
          <table class="invitation-table">
            <tr
              v-for="invitation in invitations"
              :key="invitation.invitationId"
              class="invite-row"
            >
              <td class="invite-group-name">{{ invitation.groupName }}</td>
              <td><button @click="aboutInvitation(invitation)">About</button></td>
              <td><RouterLink to="/Invitations"><button>View</button></RouterLink></td>
            </tr>
          </table>
        </div>
      </div>
    </div>
    <div class="center-div">
      <h3>Share your Recipes with family/friends</h3>
      <div>
        <button class="edit-button" @click="openRecipeEditing">
          View/Edit Your Recipes
        </button>
      </div>
    </div>
    <div class="right-div">
      <h3>Your Recipes</h3>
      <RouterLink to="/CreateRecipe">
        <button class="green-buttons">Create Recipe</button></RouterLink
      ><br /><br />
      <div class="drop-down-list" @click="recipesHidden = !recipesHidden">
        Your Recipes &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <span v-if="recipesHidden">&dtrif;</span>
        <span v-else> &utrif; </span>
      </div>
      <br />
      <div v-if="!recipesHidden && (usersRecipes.length > 0)" id="recipes-drop-down" class="drop-down">
        <table>
          <div v-for="recipe in usersRecipes" :key="recipe.id">
            <td class="name-column">&nbsp;{{ recipe.name }}</td>
            <td class="edit-column">
              <span class="edit-span">
                <button class="edit-each-row" @click="editRecipe(recipe)">
                  Edit
                </button>
              </span>
            </td>
          </div>
        </table>
      </div>
      <div v-if="!recipesHidden && (usersRecipes.length == 0)"> 
        <h4 class="no-groups-created">You have no recipes. <router-link to="/CreateRecipe">Create A Recipe?</router-link></h4>
      </div>
    </div>
  </div>
  <InvitationInfoModal v-if="isAboutModalClicked" :invitationInformation="invitationInfo" @close="closeAboutInvitationModal" />
</template>

<script setup>
import { onMounted, ref } from "vue";
import { useRouter } from "vue-router";
import { useAccountStore } from "@/stores/accountStore";
import { InvitationRequest } from "@/requests/invitation-request";
import { RecipeRequest } from "@/requests/recipe-request";
import { GroupRequest } from "@/requests/group-request";
import InvitationInfoModal from "@/components/InvitationInfoModal.vue";

const router = useRouter();
const accountStore = useAccountStore();
const invitationRequest = new InvitationRequest();
const groupRequest = new GroupRequest();

const groupsHidden = ref(true);
const invitationsHidden = ref(true);
const recipesHidden = ref(true);
const isAboutModalClicked = ref(false);
const invitationInfo = ref({});

const usersGroups = ref([]);
const invitations = ref([]);
const usersRecipes = ref([]);

const openGroupCreation = function () {
  router.push("/CreateGroup");
};

const openRecipeEditing = function () {
  router.push("/Recipes");
};

const editGroup = function (group) {
  router.push(`Group/${group.groupId}`)
};

const editRecipe = function (recipe) {
  router.push({
    name: "EditSingleRecipe",
    params: {
      recipeId: recipe.id,
    },
  });
};


const closeAboutInvitationModal = async function() {
  isAboutModalClicked.value = false;
}

const aboutInvitation = function (invitation) {
  isAboutModalClicked.value = true;
  invitationInfo.value = invitation;
}

onMounted(async () => {
  const groupsOwnedByUser = await groupRequest.getGroupsOwnedByUser(accountStore.currentUserId);
  const groupsUserIsMember = await groupRequest.getGroupsWhereUserIsMember(accountStore.currentUserId);
  usersGroups.value = groupsOwnedByUser.concat(groupsUserIsMember);

  usersRecipes.value = await new RecipeRequest().getUserRecipes(accountStore.currentUserId);
  
  invitations.value = await invitationRequest.getInvitationsToUser(accountStore.currentUserId);
});
</script>

<style>
.home-page-title {
  justify-content: center;
  text-align: center;
}

.home-page {
  display: flex;
  justify-content: space-between;
  flex-direction: row;
  flex-wrap: wrap;
}

.home-page > div {
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

.home-page > div:hover {
  border: 3px solid #c7d6d5;
}

.center-div {
  background-color: #c7d6d5;
}

.drop-down {
  min-height: 300px;
  max-height: 300px;
  flex-wrap: wrap;
  overflow-y: auto;
  border-top: none;
  border: 3px solid #c7d6d5;
  border-radius: 5px;
  text-align: left;
}

.right-div h3 {
  color: #043565;
}

.green-buttons {
  min-height: 30px;
  min-width: 100px;
  background-color: #c7d6d5;
  border-radius: 10px;
  border: 1px solid #043565;
}

.green-buttons:hover {
  box-shadow: 0 4px 4px #c7d6d5;
  cursor: pointer;
}

.edit-button {
  min-height: 30px;
  min-width: 100px;
  border-radius: 10px;
  border: 1px solid #043565;
}

.edit-button:hover {
  box-shadow: 0 2px 2px lightslategrey;
  cursor: pointer;
}

.drop-down-list {
  border: 1px solid #6d7275;
  border-radius: 8px;
  background-color: #c7d6d5;
  width: 100%;
  font-weight: bold;
  color: #043565;
}

.drop-down-list:hover {
  cursor: pointer;
  box-shadow: 1px 1px 1px gray;
}

.name-column {
  width: 600px;
}
.edit-column {
  width: 5%;
}

.edit-each-row {
  min-height: 20px;
  min-width: 80px;
  background-color: #c7d6d5;
  border-radius: 10px;
  border: 1px solid #043565;
}

.edit-each-row:hover {
  box-shadow: 0 4px 4px #c7d6d5;
  cursor: pointer;
}

.invitation-table {
  width: 100%;
  border-collapse: collapse;
}
.invite-group-name {
  width: 70%;
}
</style>
