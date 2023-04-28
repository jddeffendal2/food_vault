using FoodVaultApi.Models;

namespace FoodVaultApi.DTO
{
    public class UserDTO
    {
        public string userId { get; set; }
        public string username { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }

        public static User ToUser(UserDTO dto)
        {
            return new User();
        }

        public static UserDTO ToDTO(User user)
        {
            return new UserDTO
            {
                userId = user.Id,
                username = user.Username,
                firstName = user.FirstName,
                lastName = user.LastName,
                email = user.Email
            };
        }
    }
}
