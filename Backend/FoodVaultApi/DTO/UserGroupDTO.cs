using FoodVaultApi.Models;

namespace FoodVaultApi.DTO
{
    public class UserGroupDTO
    {
        public string userId { get; set; }
        public string groupId { get; set; }
        public bool canAddRecipes { get; set; }
        public bool canAddUsers { get; set; }

        public static UserGroupDTO ToDTO(UserGroup userGroup)
        {
            return new UserGroupDTO
            {
                userId = userGroup.UserId,
                groupId = userGroup.GroupId,
                canAddRecipes = userGroup.CanAddRecipes,
                canAddUsers = userGroup.CanAddUsers
            };
        }
    }

    public class GroupMembersDTO
    {
        public UserDTO owner { get; set; }
        public List<UserDTO> members { get; set; }
    }
}
