﻿using FoodVaultApi.DTO;
using FoodVaultApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace FoodVaultApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly FoodVaultDbContext _context;
        private IConfiguration _configuration;

        public UserController(IConfiguration config, FoodVaultDbContext context)
        {
            _configuration = config;
            _context = context;
        }

        /// <summary>
        /// Post endpoint to save a new user to the database
        /// </summary>
        /// <param name="userSignupInfo">Signup information passed from the user</param>
        /// <returns>JSON regarding success status</returns>
        [AllowAnonymous]
        [HttpPost("Signup")]
        public IActionResult SignUp(UserSignup userSignupInfo)
        {
            var usernameInUse = _context.Users.FirstOrDefault(x => x.Username.ToLower().Equals(userSignupInfo.Username.ToLower())) != null;
            if (usernameInUse)
                return BadRequest(new { success = false, message = "A user with this username already exists." });

            var emailInUse = _context.Users.FirstOrDefault(x => x.Email.ToLower().Equals(userSignupInfo.Email.ToLower())) != null;
            if (emailInUse)
                return BadRequest(new { success = false, message = "A user with this email already exists." });

            byte[] salt;
            byte[] hashedPassword;
            HashPassword(userSignupInfo.Password, out hashedPassword, out salt);

            var newUser = new User
            {
                Id = Guid.NewGuid().ToString().ToUpper(),
                Email = userSignupInfo.Email,
                Username = userSignupInfo.Username,
                FirstName = userSignupInfo.FirstName,
                LastName = userSignupInfo.LastName,
                PasswordHash = Convert.ToBase64String(hashedPassword),
                PasswordSalt = Convert.ToBase64String(salt),
                BirthDate = userSignupInfo.BirthDate.ToUniversalTime(),
                CreatedDate = DateTime.UtcNow
            };
            _context.Users.Add(newUser);
            _context.SaveChanges();

            return Ok(new { success = true, message = "Successfully created an account!" });
        }

        /// <summary>
        /// Post endpoint to authenticate a user attempting to sign in
        /// </summary>
        /// <param name="userLoginInfo">The login information supplied by the user</param>
        /// <returns>JSON regarding success status</returns>
        [AllowAnonymous]
        [HttpPost("Authenticate")]
        public IActionResult Authenticate(UserLogin userLoginInfo)
        {
            var user = _context.Users.FirstOrDefault(x => x.Email.ToLower().Equals(userLoginInfo.EmailUsername.ToLower()) ||
                x.Username.ToLower().Equals(userLoginInfo.EmailUsername.ToLower()));

            // If there is no user with the given username or email
            if (user == null)
                return BadRequest("Invalid username, email, or password.");

            // If given password does not match password associated with user
            if (!VerifyPassword(userLoginInfo.Password, Convert.FromBase64String(user.PasswordHash), Convert.FromBase64String(user.PasswordSalt)))
                return BadRequest("Invalid username, email, or password");

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString().ToUpper()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim("UserId", user.Id),
                new Claim("Username", user.Username),
                new Claim("Email", user.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: signIn);

            return Ok(new
            { 
                token = new JwtSecurityTokenHandler().WriteToken(token),
                userId = user.Id,
            });
        }

        /// <summary>
        /// Get a specific user by their ID
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("{userId}")]
        public IActionResult GetUser(string userId)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == userId);
            
            if (user == null) return NotFound();

            return Ok(UserDTO.ToDTO(user));
        }

        private void HashPassword(string password, out byte[] passwordHash, out byte[] salt)
        {
            using (var hmac = new HMACSHA512())
            {
                salt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPassword(string password, byte[] hash, byte[] salt)
        {
            using (var hmac = new HMACSHA512(salt))
            {
                var newPasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < newPasswordHash.Length; i++)
                {
                    if (newPasswordHash[i] != hash[i])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Search users given a searchTerm
        /// Searches by first name, last name, username, and email
        /// </summary>
        /// <param name="user">The user id sending the search request</param>
        /// <param name="searchTerm">The term to search by</param>
        /// <returns></returns>
        [HttpGet("SearchUsers")]
        public IActionResult SearchUsers(string user, string searchTerm) {
            var users = _context.Users
                .Where(x => 
                    !x.Id.ToLower().Equals(user.ToLower()) &&
                    (x.FirstName.Contains(searchTerm) ||
                    x.LastName.Contains(searchTerm) ||
                    x.Username.Contains(searchTerm) ||
                    x.Email.Contains(searchTerm)))
                .Select(UserDTO.ToDTO);
                        
            return Ok(users);
        }
    }
}
