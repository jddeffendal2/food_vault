using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace FoodVaultApi.Hubs
{
    [Authorize]
    public class GroupHub : Hub
    {
        /// <summary>
        /// Add the device calling this method to the specified group
        /// (call when logging in, when joining group, etc)
        /// (Since each FoodVault account could be accessing from multiple devices, add the connectionid to the group instead of the account id)
        /// </summary>
        /// <param name="groupId">The ID of the FoodVault group to add the current user connection to</param>
        /// <returns></returns>
        public async Task AddClientToGroup(string groupId)
        {
            var temp = Context.User;
            await Groups.AddToGroupAsync(Context.ConnectionId, groupId);
        }

        /// <summary>
        /// Remove the current user connection from the specified group
        /// (call on log out, leave group, etc)
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public async Task RemoveClientFromGroup(string groupId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupId);
        }

        public async Task InviteUserToGroup(string groupId, string user)
        {
            await Clients.User(user).SendAsync("InviteReceived", groupId);
        }

        // TODO: Send notification to group when recipe is added
        // TODO: Send notification to group when user joins
    }
}
