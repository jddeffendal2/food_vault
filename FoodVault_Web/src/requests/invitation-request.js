import { Request } from "@/requests/request";

export default class InvitationRequest extends Request {
  constructor() {
    super("Invitation");
  }

  sendRequest = async function(data) {
    console.log("sending request...")
    return await this.makeRequest("SendInvitation", "POST", data);
  }
}