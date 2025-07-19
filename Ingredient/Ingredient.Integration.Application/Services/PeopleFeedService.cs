using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ingredient.Integration.Application.Interfaces;
using Ingredient.Integration.Domain.Entities;

namespace Ingredient.Integration.Application.Services
{
    public class PeopleFeedService : IPeopleFeedService
    {
        public Task<List<(string RecipeName, int Quantity)>> PeopleFeed(List<Recipe> recipes)
        {
            var result = new List<(string RecipeName, int Quantity)>();

            //available ingridient quanties map 
            var ingredientMap = recipes
            .SelectMany(r => r.Ingredients)
            .Select(ri => ri.Ingredient!)
            .Distinct()
            .ToDictionary(i => i.Id, i => i.QuantityAvailable);

            // sort recipe by efficiency 
            var sortedRecipes = recipes
           .OrderByDescending(r => (double)r.PeopleFed / r.Ingredients.Sum(i => i.RequiredQuantity))
           .ToList();

            foreach (var recipe in sortedRecipes)
            {
                int maxCount = int.MaxValue;

                foreach (var ri in recipe.Ingredients)
                {
                    if (!ingredientMap.ContainsKey(ri.IngredientId))
                    {
                        maxCount = 0;
                        break;
                    }

                    int available = ingredientMap[ri.IngredientId];
                    int canMake = available / ri.RequiredQuantity;

                    maxCount = Math.Min(maxCount, canMake);
                }

                if (maxCount > 0)
                {
                    result.Add((recipe.Name, maxCount));

                    // minus ingredients used
                    foreach (var ri in recipe.Ingredients)
                    {
                        ingredientMap[ri.IngredientId] -= ri.RequiredQuantity * maxCount;
                    }
                }
            }
            return Task.FromResult(result);
        }

 
    }
}