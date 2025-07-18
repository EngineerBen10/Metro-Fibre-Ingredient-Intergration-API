using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ingredient.Integration.Application.Interfaces;
using Ingredient.Integration.Domain.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Ingredient.Integration.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeedController : ControllerBase
    {
        private readonly IRecipeService _recipeService;

        public FeedController(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        [HttpPost("Ingredient")]
        public async Task<IActionResult> AddIngredientAsyc([FromBody] Ingredient.Integration.Domain.Entities.Ingredient ingredient)
        {

            await _recipeService.AddIngredientAsyc(ingredient);
            return Ok(new { message = "Ingredient added successfully." });
            //return  NotImplementedException()

        }

        [HttpPost("Recipe")]

        public async Task<IActionResult> AddRecipeAsync([FromBody] Recipe recipe)
        {
            await _recipeService.AddRecipeAsync(recipe);
            return Ok(new { message = "Recipe added successfully." });
        }

        [HttpGet("GetIngredients")]
        public async Task<List<Ingredient.Integration.Domain.Entities.Ingredient>> GetIngredientsAsync()
        {
            return await _recipeService.GetIngredientsAsync();
        }
        [HttpGet("GetRecipes")]
        public async Task<List<Recipe>> GetRecipesAsync()
        {
            return await _recipeService.GetRecipesAsync();
        }

    }


}