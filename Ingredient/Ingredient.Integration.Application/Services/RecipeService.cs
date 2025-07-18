using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ingredient.Integration.Application.Interfaces;
using Ingredient.Integration.Domain.Entities;
using Ingredient.Integration.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Ingredient.Integration.Application.Services
{

public class RecipeService : IRecipeService
{
    private readonly AppDbContext _context;

    public RecipeService(AppDbContext context)
    {
        _context = context;
    }

        public async Task AddIngredientAsyc(Domain.Entities.Ingredient ingredient)
        {
             if (string.IsNullOrWhiteSpace(ingredient.Name))
            throw new ArgumentException("Ingredient name cannot be empty.");

        var exists = await _context.Ingredients
            .AnyAsync(i => i.Name.ToLower() == ingredient.Name.ToLower());

        if (exists)
            throw new InvalidOperationException($"Ingredient '{ingredient.Name}' already exists.");

        _context.Ingredients.Add(ingredient);
        await _context.SaveChangesAsync();
        }


    public async Task AddRecipeAsync(Recipe recipe)
    {
        if (string.IsNullOrWhiteSpace(recipe.Name))
            throw new ArgumentException("Recipe name cannot be empty.");

        if (recipe.RecipeItems == null || !recipe.RecipeItems.Any())
            throw new ArgumentException("Recipe must include at least one ingredient.");

        var ingredientIds = recipe.RecipeItems.Select(ri => ri.IngredientId).ToList();

        var ingredients = await _context.Ingredients
            .Where(i => ingredientIds.Contains(i.Id))
            .ToDictionaryAsync(i => i.Id);

        foreach (var item in recipe.RecipeItems)
        {
            if (!ingredients.TryGetValue(item.IngredientId, out var matchedIngredient))
                throw new KeyNotFoundException($"Ingredient with ID {item.IngredientId} does not exist.");

            if (item.RequiredQuantity <= 0)
                throw new ArgumentException("Quantity required must be greater than zero.");

            item.Ingredient = matchedIngredient;
        }

        _context.Recipes.Add(recipe);
        await _context.SaveChangesAsync();
    }

            async Task<List<Domain.Entities.Ingredient>> IRecipeService.GetIngredientsAsync()
            {
                return await _context.Ingredients.ToListAsync();
            }

            async Task<List<Recipe>> IRecipeService.GetRecipesAsync()
            {
                return await _context.Recipes.ToListAsync();
            }
        }

    }
