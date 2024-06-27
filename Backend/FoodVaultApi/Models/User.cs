namespace FoodVaultApi.Models
{
    public class User
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime CreatedDate { get; set; }

        public ICollection<Group> Groups { get; set; } // User can be owner of group
        public ICollection<Recipe> Recipes { get; set; } // User can be owner of recipe
        public ICollection<UserGroup> UserGroups { get; set; } // all the groups user belongs to
        public ICollection<Invitation> SentInvitations { get; set; }
        public ICollection<Invitation> ReceivedInvitations { get; set; }
        public ICollection<RecipeView> RecipeViews { get; set; }
    }
}
