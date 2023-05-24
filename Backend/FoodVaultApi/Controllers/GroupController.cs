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
        public IActionResult Create(GroupDTO groupDto)
        {
            var group = new Group
            {
                Id = Guid.NewGuid().ToString().ToUpper(),
                UserId = groupDto.creator,
                Name = groupDto.name,
                Description = groupDto.description,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
            };

            _context.Groups.Add(group);
            _context.SaveChanges();

            return Ok();
        }

        [HttpGet("{userId}")]

        public IActionResult GetGroupsById(String userId) {
            var groups = _context.Groups.Where(x => x.UserId == userId);
                        
            return Ok(groups);
        }
    }
}
