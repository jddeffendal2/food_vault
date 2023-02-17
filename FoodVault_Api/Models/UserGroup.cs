namespace FoodVault_Api.Models
{
    public class UserGroup
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string GroupId { get; set; }

        public User User { get; set; } // User for this usergroup entry
        public Group Group { get; set; } // Group for this usergroup entry
    }
}
