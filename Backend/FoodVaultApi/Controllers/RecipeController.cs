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

        public IActionResult GetRecipesByUserId(string userId) {
            var recipes = _context.Recipes.Where(x => x.UserId == userId);
                        
            return Ok(recipes);
        }

        [HttpGet("GetRecipe/{recipeId}")]
        public IActionResult GetRecipeByRecipeId(string recipeId) {
            var recipe = _context.Recipes.FirstOrDefault(x => x.Id == recipeId);
                        
            return Ok(recipe);
        }

        [HttpGet("User/{userId}/Recipe/{recipeId}/IsAuthorized")]
        public IActionResult IsUserAuthorizedToViewRecipe(string userId, string recipeId)
        {
            var recipe = _context.Recipes.FirstOrDefault(x => x.Id == recipeId);

            if (recipe != null && recipe.UserId == userId)
                return Ok(true);

            var groupsRecipeIsSharedWith = _context.GroupRecipes
                .Where(x => x.RecipeId == recipeId)
                .Select(x => x.GroupId)
                .ToList();

            var groupsUserIsPartOf = _context.UserGroups
                .Where(x => x.UserId == userId)
                .Select(x => x.GroupId)
                .ToList();

            var recipeIsSharedWithUser = groupsUserIsPartOf.Any(x => groupsRecipeIsSharedWith.Contains(x));

            if (recipeIsSharedWithUser)
                return Ok(true);

            return Ok(false);
        }

        [HttpGet("Recipe/{recipeId}/User/{userId}/IsOwner")]
        public IActionResult IsUserOwnerOfRecipe(string recipeId, string userId)
        {
            var recipe = _context.Recipes.FirstOrDefault(x => x.Id == recipeId);

            if (recipe == null)
                return NotFound();

            return Ok(recipe.UserId == userId);
        }
    }
}
