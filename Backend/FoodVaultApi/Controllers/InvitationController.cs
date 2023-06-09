using FoodVaultApi.DTO;
using FoodVaultApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodVaultApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class InvitationController : ControllerBase
    {
        private readonly FoodVaultDbContext _context;
        private IConfiguration _configuration;

        public InvitationController(IConfiguration config, FoodVaultDbContext context)
        {
            _context = context;
            _configuration = config;
        }

        [HttpPost("SendInvitation")]
        public IActionResult InviteUserToGroup(InvitationPost invitationPost)
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

            return Ok();
        }
    }
}
