using FoodVaultApi.DTO;
using FoodVaultApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodVaultApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]

    public class RecipeController : ControllerBase
    {
        private readonly FoodVaultDbContext _context;
        private IConfiguration _configuration;

        public RecipeController(IConfiguration config, FoodVaultDbContext context)
        {
            _configuration = config;
            _context = context;
        }

        [HttpPost("Create")]

        public IActionResult Create(RecipeDTO recipeDto)
        {
            var recipe = new Recipe
            {
                Id = Guid.NewGuid().ToString().ToUpper(),
                UserId = recipeDto.creator,
                Name = recipeDto.name,
                Description = recipeDto.description,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
            };

            _context.Recipes.Add(recipe);
            _context.SaveChanges();

            return Ok(recipe.Id);
        }

        [HttpGet("{userId}")]

        public IActionResult GetRecipesById(String userId) {
            var recipes = _context.Recipes.Where(x => x.UserId == userId);
                        
            return Ok(recipes);
        }

        [HttpGet("GetRecipe/{recipeId}")]

        public IActionResult GetRecipeByRecipeId(String recipeId) {
            var recipe = _context.Recipes.Where(x => x.Id == recipeId).First();
                        
            return Ok(recipe);
        }
    }
}
