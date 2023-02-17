using FoodVault_Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodVault_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RecipeController : ControllerBase
    {
        private readonly FoodVaultDbContext _context;

        public RecipeController(FoodVaultDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetAllRecipes")]
        public List<Recipe> GetRecipes()
        {
            return _context.Recipes.ToList();
        }
    }
}
