namespace food_vault_api.Models
{
    public class User
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
