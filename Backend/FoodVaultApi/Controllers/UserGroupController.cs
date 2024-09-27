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

        /// <summary>
        /// Create new UserGroup row (add user to group)
        /// </summary>
        /// <param name="userGroupDto"></param>
        /// <returns></returns>
        [HttpPost("Create")]
        public IActionResult Create(UserGroupDTO userGroupDto)
        {
            var userGroup = new UserGroup
            {
                Id = Guid.NewGuid().ToString().ToUpper(),
                UserId = userGroupDto.userId,
                GroupId = userGroupDto.groupId,
            };

            _context.UserGroups.Add(userGroup);
            _context.SaveChanges();

            return Ok();
        }

        [HttpGet("GetAllGroupsUserIsIn/{userId}")]
        public IActionResult GetAllGroupsUserIsIn(string userId)
        {
            var ownedGroups = _context.Groups
                .Where(x => x.UserId == userId)
                .Select(x => x.Id)
                .ToList();

            var groupsPartOf = _context.UserGroups
                .Where(x => x.UserId == userId)
                .Select(x => x.GroupId)
                .ToList();

            return Ok(ownedGroups.Concat(groupsPartOf));
        }

        [HttpGet("GetAll")]
        public IActionResult GetAllUserGroups()
        {
            var groups = _context.UserGroups.Select(x => x);
            return Ok(groups);
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