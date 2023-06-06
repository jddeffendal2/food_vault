using FoodVaultApi.DTO;
using FoodVaultApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodVaultApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class GroupController : ControllerBase
    {
        private readonly FoodVaultDbContext _context;
        private IConfiguration _configuration;

        public GroupController(IConfiguration config, FoodVaultDbContext context)
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
    }
}