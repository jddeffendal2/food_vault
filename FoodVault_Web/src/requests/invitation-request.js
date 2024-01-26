import { Request } from "@/requests/request";

export class InvitationRequest extends Request {
  constructor() {
    super("Invitation");
  }

  getInvitationsToUser = async function(userId) {
    return await this.makeRequest(`${userId}/Invitations`, "GET");
  }

  sendRequest = async function(data) {
    return await this.makeRequest("SendInvitation", "POST", data);
  }

  acceptInvitation = async function(invitationId) {
    return await this.makeRequest(`${invitationId}/Accept`, "POST");
  }

  rejectInvitation = async function(invitationId) {
    return await this.makeRequest(`Invitations/${invitationId}`, "DELETE")
  }
}