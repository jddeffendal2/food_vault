namespace FoodVaultApi.DTO
{
    public class UserSignup
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
    }

    public class UserLogin
    {
        public string EmailUsername { get; set; }
        public string Password { get; set; }
    }
}
