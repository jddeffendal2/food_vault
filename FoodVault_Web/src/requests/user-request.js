import { Request } from "@/requests/request";

export default class UserRequest extends Request {
  constructor() {
    super("User");
  }

  logIn = async function(data) {
    return await this.makeRequest("Authenticate", "POST", data);
  }

  signUp = async function(data) {
    return await this.makeRequest("SignUp", "POST", data);
  }

  getUser = async function(userId) {
    return this.makeAuthenticatedRequest(userId, "GET");
  }
}