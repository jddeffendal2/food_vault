import { Request } from "@/requests/request";

export default class GroupRequest extends Request {
  constructor() {
    super("Group");
  }

  createGroup = async function(groupInfo) {
    return await this.makeRequest("Create", "POST", groupInfo);
  }

  getGroupsOwnedByUser = async function(userId) {
    return await this.makeRequest(userId, "GET");
  }

  getGroupById = async function (groupId) {
    return await this.makeRequest("GetGroup/" + groupId, "GET")
  }
}