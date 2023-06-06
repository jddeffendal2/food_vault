import { Request } from "@/requests/request";

export default class UserGroupRequest extends Request {
  constructor() {
    super("UserGroup");
  }

  createGroup = async function(userGroupInfo) {
    return await this.makeAuthenticatedRequest("Create", "POST", userGroupInfo);
  }
}