using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ingredient.Integration.Domain.Entities;

namespace Ingredient.Integration.Application.Interfaces
{
    public interface IRecipeService
    {
        Task AddIngredientAsyc(Ingredient.Integration.Domain.Entities.Ingredient ingredient);
        Task<List<Ingredient.Integration.Domain.Entities.Ingredient>> GetIngredientsAsync();
        Task AddRecipeAsync(Recipe recipe);

        Task<List<Recipe>> GetRecipesAsync();
    }
}