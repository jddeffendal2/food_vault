namespace FoodVault_Api.Models
{
    public class Group
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }// owner/creator of the group
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public User User { get; set; } // Creator of group
        public ICollection<UserGroup> UserGroups { get; set; } // Every usergroup for this group
        public ICollection<GroupRecipe> GroupRecipes { get; set; } // Every recipe in this group
        public ICollection<Invitation> Invitations { get; set; } // Invitations to this group
    }
}
