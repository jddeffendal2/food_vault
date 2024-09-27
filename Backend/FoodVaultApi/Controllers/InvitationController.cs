using FoodVaultApi.DTO;
using FoodVaultApi.Hubs;
using FoodVaultApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace FoodVaultApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class InvitationController : ControllerBase
    {
        private readonly FoodVaultDbContext _context;
        private IConfiguration _configuration;
        private readonly IHubContext<GroupHub> _groupHub;

        public InvitationController(IConfiguration config, FoodVaultDbContext context, IHubContext<GroupHub> groupHub)
        {
            _context = context;
            _configuration = config;
            _groupHub = groupHub;
        }

        /// <summary>
        /// Get all open invitations sent to a user
        /// </summary>
        /// <param name="userId">The user id being invited</param>
        /// <returns></returns>
        [HttpGet("{userId}/Invitations")]
        public IActionResult GetInvitationsToUser(string userId)
        {
            var invitations = _context.Invitations
                .Where(x => x.SentTo.ToUpper().Equals(userId.ToUpper()) && !x.Accepted)
                .Select(x => InvitationDTO.ToDTO(
                    x.Id,
                    $"{x.SentFromUser.FirstName} {x.SentFromUser.LastName}",
                    x.SentFromUser.Username,
                    x.Group.Id,
                    x.Group.Name,
                    x.SentDate));

            return Ok(invitations);
        }

        [HttpPost("SendInvitation")]
        public async Task<IActionResult> InviteUserToGroup(InvitationPost invitationPost)
        {
            _context.Invitations.Add(new Invitation
            {
                Id = Guid.NewGuid().ToString().ToUpper(),
                SentFrom = invitationPost.fromUserId,
                SentTo = invitationPost.toUserId,
                SentDate = DateTime.UtcNow,
                Accepted = false,
                GroupId = invitationPost.groupId
            });
            _context.SaveChanges();

            await _groupHub.Clients.User(invitationPost.toUserId).SendAsync("InviteReceived");

            return Ok();
        }

        [HttpPost("{invitationId}/Accept")]
        public IActionResult AcceptInvitation(string invitationId)
        {
            // TODO: Do we delete the invitation? Or simply mark as accepted
            var invite = _context.Invitations.FirstOrDefault(x => x.Id.ToUpper() == invitationId.ToUpper());

            if (invite == null)
                return NotFound();

            invite.Accepted = true;
            invite.AcceptedDate = DateTime.UtcNow;

            _context.UserGroups.Add(new UserGroup
            {
                Id = Guid.NewGuid().ToString().ToUpper(),
                GroupId = invite.GroupId,
                UserId = invite.SentTo
            });

            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete("Invitations/{invitationId}")]
        public IActionResult DeleteInvitation(string invitationId) {
            var foundInvitation = _context.Invitations.FirstOrDefault(x => x.Id == invitationId);
            _context.Invitations.Remove(foundInvitation);
            _context.SaveChanges();

            return Ok();
        }
    }
}
