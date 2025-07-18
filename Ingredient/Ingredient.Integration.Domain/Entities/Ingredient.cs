using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ingredient.Integration.Domain.Entities
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public int QuantityAvailable { get; set; }
        public ICollection<RecipeIngredient> Recipes { get; set; } = new List<RecipeIngredient>();
    
    }
}