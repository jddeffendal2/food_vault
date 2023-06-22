import { Request } from "@/requests/request";

export default class UserGroupRequest extends Request {
  constructor() {
    super("UserGroup");
  }

  createGroup = async function(userGroupInfo) {
    return await this.makeRequest("Create", "POST", userGroupInfo);
  }
  getAllUserGroups = async function() {
    return await this.makeRequest("GetAll", "GET")
  }
}