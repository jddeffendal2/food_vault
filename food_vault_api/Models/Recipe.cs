using System.Text.RegularExpressions;

namespace food_vault_api.Models
{
    public class Recipe
    {
        public string Id { get; set; }
        public string Title { get; set; }

        public DateTime CreatedDate { get; set; }
        public string GroupId { get; set; }
        public string UserId { get; set; }
    }
}
