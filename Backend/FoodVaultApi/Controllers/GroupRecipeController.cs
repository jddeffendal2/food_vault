﻿using FoodVaultApi.DTO;
using FoodVaultApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodVaultApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class GroupRecipeController : ControllerBase
    {
        private readonly FoodVaultDbContext _context;
        private IConfiguration _configuration;

        public GroupRecipeController(IConfiguration config, FoodVaultDbContext context)
        {
            _configuration = config;
            _context = context;
        }

        [HttpPost("Create")]
        public IActionResult Create(GroupRecipeDTO groupRecipeDto)
        {
            var groupRecipe = new GroupRecipe
            {
                Id = Guid.NewGuid().ToString().ToUpper(),
                GroupId = groupRecipeDto.groupId,
                RecipeId = groupRecipeDto.recipeId
            };

            _context.GroupRecipes.Add(groupRecipe);
            _context.SaveChanges();

            return Ok();
        }

        [HttpGet("{groupId}")]
        public IActionResult GetRecipesByGroupId(string groupId)
        {
            var recipesInGroup = _context.GroupRecipes
                .Where(x => x.GroupId == groupId)
                .Select(x => x.RecipeId);

            var recipes = _context.Recipes.Where(x => recipesInGroup.Contains(x.Id));

            return Ok(recipes);
        }

        [HttpGet("GetGroupsThatContainRecipe/{recipeId}")]
        public IActionResult GetGroupsThatContainRecipe(string recipeId)
        {
            var groupsThatContainRecipe = _context.GroupRecipes.Where(x => x.RecipeId == recipeId);

            return Ok(groupsThatContainRecipe);
        }
    }
}