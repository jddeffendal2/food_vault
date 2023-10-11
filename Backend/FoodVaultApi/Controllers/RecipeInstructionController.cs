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

        [HttpGet("Recipe/{recipeId}")]
        public IActionResult GetAllInstructionsForRecipe(string recipeId)
        {
            var recipe = _context.Recipes.FirstOrDefault(x => x.Id.ToUpper() == recipeId.ToUpper());

            if (recipe == null)
                return NotFound();

            var instructions = _context.RecipeInstructions
                .Where(x => x.RecipeId.ToUpper() == recipeId.ToUpper())
                .OrderBy(x => x.SortOrder)
                .Select(RecipeInstructionDTO.ToDTO);

            return Ok(instructions);
        }

        [HttpPut("RecipeInstructions/{recipeId}/InstructionId/{instructionId}")]
        public IActionResult UpdateRecipeInstructions(string recipeId, string instructionId, RecipeInstructionDTO recipeInstructionDTO) {
            var recipeInstruction = _context.RecipeInstructions.FirstOrDefault(x => x.RecipeId == recipeId && x.Id == instructionId);
            recipeInstruction.Text = recipeInstructionDTO.text;
            recipeInstruction.SortOrder = recipeInstructionDTO.sortOrder;
            _context.SaveChanges();

            return Ok(recipeInstruction);
        }

        [HttpDelete("RecipeInstructions/{instructionId}")]
        public IActionResult DeleteRecipeIngredient(string instructionId) {
             var recipeInstruction = _context.RecipeInstructions.FirstOrDefault(x => x.Id == instructionId);
             _context.RecipeInstructions.Remove(recipeInstruction);
             _context.SaveChanges();

             return Ok();
        }
    }
}