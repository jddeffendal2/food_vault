namespace FoodVaultApi.DTO
{
    public class UserGroupDTO
    {
        public string userId { get; set; }
        public string groupId { get; set; }
    }

    public class UserGroupDetailedDTO
    {
        public string groupId { get; set; }
        public string groupName { get; set; }
    }
}
