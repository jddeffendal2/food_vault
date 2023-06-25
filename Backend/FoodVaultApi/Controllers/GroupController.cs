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
        public IActionResult Create(GroupPostDTO groupPostDto)
        {
            var group = new Group
            {
                Id = Guid.NewGuid().ToString().ToUpper(),
                UserId = groupPostDto.ownerId,
                Name = groupPostDto.name,
                Description = groupPostDto.description,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
            };

            _context.Groups.Add(group);
            _context.SaveChanges();

            return Ok();
        }

        [HttpGet("GetGroup/{groupId}")]
        public IActionResult GetGroupByGroupId(string groupId) {
            var group = _context.Groups.FirstOrDefault(x => x.Id.ToUpper() == groupId.ToUpper());

            if (group == null)
                return NotFound();

            var owner = _context.Users.FirstOrDefault(x => x.Id.ToUpper() == group.UserId.ToUpper());
            UserDTO ownerDto = null;
            if (owner != null) ownerDto = UserDTO.ToDTO(owner);

            var groupUsers = _context.UserGroups
                .Where(x => x.GroupId.ToUpper() == groupId.ToUpper())
                .Select(x => _context.Users.FirstOrDefault(y => y.Id.ToUpper() == x.UserId.ToUpper()));

            var members = new List<UserDTO>();
            foreach (var user in groupUsers)
            {
                if (user == null) continue;
                members.Add(UserDTO.ToDTO(user));
            }

            var detailedGroupInfo = new GroupDetailedDTO
            {
                groupId = groupId,
                ownerId = group.UserId,
                name = group.Name,
                description = group.Description,
                createdDate = group.CreatedDate,
                updatedDate = group.UpdatedDate,
                owner = ownerDto,
                members = members
            };

            return Ok(detailedGroupInfo);
        }

        [HttpGet("{userId}/Owner")]
        public IActionResult GetGroupsByOwnerId(string userId) {
            var groups = _context.Groups
                .Where(x => x.UserId.ToUpper() == userId.ToUpper())
                .Select(GroupDTO.ToDTO);
                        
            return Ok(groups);
        }

        [HttpGet("{userId}/Member")]
        public IActionResult GetGroupsWhereUserIsMember(string userId)
        {
            var groups = _context.UserGroups
                .Where(x => x.UserId.ToUpper() == userId.ToUpper())
                .Select(x => GroupDTO.ToDTO(x.Group));

            return Ok(groups);
        }
    }
}
