namespace FoodVault.Api.Data.Models
{
    public class Group
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string OwnerId { get; set; } // the ID of the owner/creator of the group
        public DateTime CreatedDate { get; set; }
    }
}
