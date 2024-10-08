﻿using FoodVaultApi.DTO;
using FoodVaultApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodVaultApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]

    public class RecipeIngredientController : ControllerBase
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
                RecipeId = recipeIngredientDto.recipeId,
                Name = recipeIngredientDto.name,
                Quantity = recipeIngredientDto.quantity,
                UnitOfMeasurement = recipeIngredientDto.unitOfMeasurement,
                SortOrder = recipeIngredientDto.sortOrder
            };

            _context.RecipeIngredients.Add(recipeIngredient);
            _context.SaveChanges();

            return Ok();
        }

        [HttpGet("Recipe/{recipeId}")]
        public IActionResult GetAllIngredientsForRecipe(string recipeId)
        {
            var recipe = _context.Recipes.FirstOrDefault(x => x.Id.ToUpper() == recipeId.ToUpper());

            if (recipe == null)
                return NotFound();

            var ingredients = _context.RecipeIngredients
                .Where(x => x.RecipeId.ToUpper() == recipeId.ToUpper())
                .OrderBy(x => x.SortOrder)
                .Select(RecipeIngredientGetDTO.ToDTO);

            return Ok(ingredients);
        }

        [HttpPut("RecipeIngredients/{recipeId}/IngredientId/{ingredientId}")]
        public IActionResult UpdateRecipeIngredients(string recipeId, string ingredientId, RecipeIngredientDTO recipeIngredientDto) {
            var recipeIngredient = _context.RecipeIngredients.FirstOrDefault(x => x.RecipeId == recipeId && x.Id == ingredientId);
            recipeIngredient.Name = recipeIngredientDto.name;
            recipeIngredient.Quantity = recipeIngredientDto.quantity;
            recipeIngredient.UnitOfMeasurement = recipeIngredientDto.unitOfMeasurement;
            recipeIngredient.SortOrder = recipeIngredientDto.sortOrder;
            _context.SaveChanges();

            return Ok(recipeIngredient);
        }

        [HttpDelete("RecipeIngredients/{ingredientId}")]
        public IActionResult DeleteRecipeIngredient(string ingredientId) {
             var recipeIngredient = _context.RecipeIngredients.FirstOrDefault(x => x.Id == ingredientId);
             _context.RecipeIngredients.Remove(recipeIngredient);
             _context.SaveChanges();

             return Ok();
        }
    }
}
