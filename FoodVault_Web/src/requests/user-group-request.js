import { Request } from "@/requests/request";

export class UserGroupRequest extends Request {
  constructor() {
    super("UserGroup");
  }

  getUserGroup = async function (groupId, userId) {
    return await this.makeRequest(`${groupId}/user/${userId}`, "GET")
  }

  updateUserGroupPermissions = async function (userId, groupId, canAddRecipes, canAddUsers) {
    return await this.makeRequest("UpdatePermissions", "PUT", { userId, groupId, canAddRecipes, canAddUsers })
  }

  getAllUserGroups = async function () {
    return await this.makeRequest("GetAll", "GET");
  }

  getAllUsersInSpecificGroups = async function (groupId) {
    return await this.makeRequest("GetUsersInSpecificGroups/" + groupId, "GET");
  }

  removeUserFromGroup = async function (groupId, userId) {
    return await this.makeRequest(`Group/${groupId}/User/${userId}/Remove`, "DELETE");
  }

  getAllGroupsUserIsIn = async function (userId) {
    return await this.makeRequest(`GetAllGroupsUserIsIn/${userId}`, "GET");
  }
}