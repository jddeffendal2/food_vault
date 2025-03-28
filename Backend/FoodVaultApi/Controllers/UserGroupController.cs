using FoodVaultApi.DTO;
using FoodVaultApi.Hubs;
using FoodVaultApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace FoodVaultApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserGroupController : ControllerBase
    {
        private readonly FoodVaultDbContext _context;
        private IConfiguration _configuration;
        private readonly IHubContext<GroupHub> _groupHub;

        public UserGroupController(IConfiguration config, FoodVaultDbContext context, IHubContext<GroupHub> groupHub)
        {
            _configuration = config;
            _context = context;
            _groupHub = groupHub;
        }

        [HttpGet("{groupId}/user/{userId}")]
        public IActionResult GetUserGroup(string groupId, string userId)
        {
            var userGroup = _context.UserGroups.FirstOrDefault(x => x.GroupId.ToLower() == groupId.ToLower() && x.UserId.ToLower() == userId.ToLower());
            if (userGroup == null)
                return NotFound();

            return Ok(UserGroupDTO.ToDTO(userGroup));
        }

        [HttpPut("UpdatePermissions")]
        public IActionResult UpdateUserGroupPermissions(UserGroupDTO userGroupDto)
        {
            var userGroup = _context.UserGroups
                .FirstOrDefault(x => x.UserId.ToLower() == userGroupDto.userId.ToLower() && x.GroupId.ToLower() == userGroupDto.groupId.ToLower());

            if (userGroup == null)
                return NotFound();

            userGroup.CanAddRecipes = userGroupDto.canAddRecipes;
            userGroup.CanAddUsers = userGroupDto.canAddUsers;

            _context.SaveChanges();
            return Ok();
        }

        [HttpGet("GetAllGroupsUserIsIn/{userId}")]
        public IActionResult GetAllGroupsUserIsIn(string userId)
        {
            var groupsPartOf = _context.UserGroups
                .Where(x => x.UserId == userId)
                .Select(x => x.GroupId)
                .ToList();

            return Ok(groupsPartOf);
        }

        [HttpGet("GetAll")]
        public IActionResult GetAllUserGroups()
        {
            return Ok(_context.UserGroups.ToList());
        }

        [HttpGet("GetUsersInSpecificGroups/{groupId}")]
        public IActionResult GetUsersInSpecificGroups(string groupId)
        {
            var usersInGroup = _context.UserGroups
                .Where(x => x.GroupId == groupId)
                .Select(x => x.UserId);

            return Ok(usersInGroup);
        }

        [HttpDelete("Group/{groupId}/User/{userId}/Remove")]
        public IActionResult RemoveUserFromGroup(string groupId, string userId)
        {
            var userInGroup = _context.UserGroups.FirstOrDefault(x => x.GroupId == groupId && x.UserId == userId);

            if (userInGroup == null)
                return NotFound();

            _context.UserGroups.Remove(userInGroup);
            _context.SaveChanges();

            return Ok();
        }
    }
}