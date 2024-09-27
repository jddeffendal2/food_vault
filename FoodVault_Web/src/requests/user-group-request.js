import { Request } from "@/requests/request";

export class UserGroupRequest extends Request {
  constructor() {
    super("UserGroup");
  }

  createGroup = async function (userGroupInfo) {
    return await this.makeRequest("Create", "POST", userGroupInfo);
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