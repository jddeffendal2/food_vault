using FoodVaultApi.Models;

namespace FoodVaultApi.DTO
{
    public class GroupDTO
    {
        public string groupId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string ownerId { get; set; }
        public DateTime createdDate { get; set; }
        public DateTime updatedDate { get; set; }

        public static GroupDTO ToDTO(Group group)
        {
            return new GroupDTO
            {
                groupId = group.Id,
                name = group.Name,
                description = group.Description,
                ownerId = group.UserId,
                createdDate = group.CreatedDate,
                updatedDate = group.UpdatedDate
            };
        }
    }

    public class GroupDetailedDTO
    {
        public string groupId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string ownerId { get; set; }
        public DateTime createdDate { get; set; }
        public DateTime updatedDate { get; set; }
        public UserDTO? owner { get; set; }
        public List<UserDTO>? members { get; set; }
    }
    public class GroupPostDTO
    {
        public string name { get; set; }
        public string description { get; set; }
        public string ownerId { get; set; }
    }

    public class GroupOverviewDTO
    {
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public bool isOwner { get; set; }
        public int userCount { get; set; }
        public int recipeCount { get; set; }
    }
}
