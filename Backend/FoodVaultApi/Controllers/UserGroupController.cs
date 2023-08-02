using FoodVaultApi.DTO;
using FoodVaultApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodVaultApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserGroupController : ControllerBase
    {
        private readonly FoodVaultDbContext _context;
        private IConfiguration _configuration;

        public UserGroupController(IConfiguration config, FoodVaultDbContext context)
        {
            _configuration = config;
            _context = context;
        }

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
    }
}