using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ingredient.Integration.Domain.Entities
{
    public class RecipeItem
    {
        public int RecipeId { get; set; }

        public Recipe Recipe { get; set; } = default!;

        public int IngredientId { get; set; }

        public Ingredient Ingredient { get; set; } = default!;
    }
}