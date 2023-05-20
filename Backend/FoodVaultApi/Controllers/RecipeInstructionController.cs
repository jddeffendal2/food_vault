using FoodVaultApi.DTO;
using FoodVaultApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodVaultApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]

    public class RecipeInstructionController : ControllerBase
    {
        private readonly FoodVaultDbContext _context;
        private IConfiguration _configuration;

        public RecipeInstructionController(IConfiguration config, FoodVaultDbContext context)
        {
            _configuration = config;
            _context = context;
        }

        [HttpPost("Create")]

        public IActionResult Create(RecipeInstructionDTO recipeInstructionDto)
        {
            var recipeInstruction = new RecipeInstruction
            {
                Id = Guid.NewGuid().ToString().ToUpper(),
                RecipeId = recipeInstructionDto.recipeId,
                Text = recipeInstructionDto.text,
                SortOrder = recipeInstructionDto.sortOrder
            };

            _context.RecipeInstructions.Add(recipeInstruction);
            _context.SaveChanges();

            return Ok();
        }
    }
}