using FoodVaultApi.DTO;
using FoodVaultApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodVaultApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]

    public class RecipeViewController : ControllerBase 
    {
        private readonly FoodVaultDbContext _context;
        private IConfiguration _configuration;

        public RecipeViewController(IConfiguration config, FoodVaultDbContext context)
        {
            _configuration = config;
            _context = context;
        }

        [HttpPost("Create")]
        public IActionResult Create(RecipeViewDTO recipeViewDto)
        {
            var recipeView = new RecipeView
            {
                Id = Guid.NewGuid().ToString().ToUpper(),
                UserId = recipeViewDto.userId,
                RecipeId = recipeViewDto.recipeId,
            };

            _context.RecipeViews.Add(recipeView);
            _context.SaveChanges();

            return Ok();
        }

        [HttpGet("Views")]
        public IActionResult GetViews(string recipeId) 
        {
            var views = _context.RecipeViews.Count(x => x.RecipeId == recipeId);

            return Ok(views);
        }
    }
}