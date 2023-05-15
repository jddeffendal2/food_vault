using FoodVaultApi.DTO;
using FoodVaultApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodVaultApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]

    public class RecipeIngredientController
    {
        private readonly FoodVaultDbContext _context;
        private IConfiguration _configuration;

        public RecipeIngredientController(IConfiguration config, FoodVaultDbContext context)
        {
            _configuration = config;
            _context = context;
        }

        [HttpPost("Create")]

        public IActionResult Create(RecipeIngredientDTO recipeIngredientDto)
        {
            var recipeIngredient = new RecipeIngredient
            {
                Id = Guid.NewGuid().ToString().ToUpper(),
                Name = recipeIngredientDto.name,
                Quantity = recipeIngredientDto.quantity,
                UnitOfMeasurement = recipeIngredientDto.unitOfMeasurement,
                SortOrder = recipeIngredientDto.sortOrder
            };

            _context.RecipeIngredients.Add(recipeIngredient);
            _context.SaveChanges();

            return Ok();
        }
    }
}
