import { Request } from "@/requests/request";

export default class UserRequest extends Request {
  constructor() {
    super("User");
  }

  logIn = async function(data) {
    return await this.makeAnonymousRequest("Authenticate", "POST", data);
  }

  signUp = async function(data) {
    return await this.makeAnonymousRequest("SignUp", "POST", data);
  }

  getUser = async function(userId) {
    return this.makeRequest(userId, "GET");
  }

  searchUsers = async function(userId, searchTerm) {
    return this.makeRequest("SearchUsers", "GET", { user: userId, searchTerm });
  }
}