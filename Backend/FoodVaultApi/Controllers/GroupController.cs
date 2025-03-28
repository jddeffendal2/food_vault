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
            // Create the actual group
            var group = new Group
            {
                Id = Guid.NewGuid().ToString().ToUpper(),
                UserId = groupPostDto.ownerId,
                Name = groupPostDto.name,
                Description = groupPostDto.description,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
            };

            // Create usergroup entry for the owner
            var userGroup = new UserGroup
            {
                Id = Guid.NewGuid().ToString().ToUpper(),
                UserId = groupPostDto.ownerId,
                GroupId = group.Id,
                CanAddRecipes = true,
                CanAddUsers = true
            };

            _context.Groups.Add(group);
            _context.UserGroups.Add(userGroup);
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

            // Find all members of group other than the owner
            var groupUsers = _context.UserGroups
                .Where(x => x.GroupId.ToUpper() == groupId.ToUpper() && x.UserId.ToUpper() != group.UserId.ToUpper())
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
            var allGroupsContainingUser = _context.UserGroups
                .Where(x => x.UserId.ToUpper() == userId.ToUpper())
                .Select(x => GroupDTO.ToDTO(x.Group))
                .ToList();

            var groupIdsOwnedByUser = _context.Groups.Where(x => x.UserId.ToUpper() == userId.ToUpper()).Select(x => x.Id).ToList();

            var groupsNotOwnedByUser = allGroupsContainingUser.Where(x => !groupIdsOwnedByUser.Contains(x.groupId));

            return Ok(groupsNotOwnedByUser);
        }

        [HttpGet("GetGroups/{userId}")]
        public IActionResult GetAllGroupDetailsForUser(string userId)
        {
            var groupsOverview = new List<GroupOverviewDTO>();
            var ownedGroupIds = new HashSet<string>();
            var memberGroupIds = new HashSet<string>();

            // Get every group id for groups owned by user
            _context.Groups
                .Where(x => x.UserId.ToUpper() == userId.ToUpper())
                .ToList()
                .ForEach(x => ownedGroupIds.Add(x.Id));

            // Get every group id for groups where user is only a member, not owner
            _context.UserGroups
                .Where(x => x.UserId.ToUpper() == userId.ToUpper() && !ownedGroupIds.Contains(x.GroupId))
                .ToList()
                .ForEach(x => memberGroupIds.Add(x.GroupId));

            foreach (var groupId in ownedGroupIds.Concat(memberGroupIds))
            {
                var group = _context.Groups.FirstOrDefault(x => x.Id.ToUpper() == groupId.ToUpper());
                if (group == null) continue;

                var userCount = _context.UserGroups.Count(x => x.GroupId.ToUpper() == groupId.ToUpper());
                var recipeCount = _context.GroupRecipes.Count(x => x.GroupId.ToUpper() == groupId.ToUpper());

                groupsOverview.Add(new GroupOverviewDTO
                {
                    id = group.Id,
                    name = group.Name,
                    description = group.Description,
                    isOwner = ownedGroupIds.Contains(groupId),
                    userCount = userCount - 1, // - 1 to subtract owner from member count
                    recipeCount = recipeCount,
                });
            }
            return Ok(groupsOverview);
        }
    }
}
