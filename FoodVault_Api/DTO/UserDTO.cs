using FoodVault_Api.Models;

namespace FoodVault_Api.DTO
{
    public class UserDTO
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthdate { get; set; }

        //public User ToUser(UserDTO dto)
        //{
        //    return new User
        //    {
        //        FirstName = 
        //    }
        //}
    }
}
