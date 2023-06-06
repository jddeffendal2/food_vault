import { Request } from "@/requests/request";

export default class GroupRequest extends Request {
  constructor() {
    super("Group");
  }

  createGroup = async function(groupInfo) {
    return await this.makeAuthenticatedRequest("Create", "POST", groupInfo);
  }

  getGroupsOwnedByUser = async function(userId) {
    return await this.makeAuthenticatedRequest(userId, "GET");
  }

  getGroupById = async function (groupId) {
    return await this.makeAuthenticatedRequest("GetGroup/" + groupId, "GET")
  }
}