using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ingredient.Integration.Domain.Entities
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public int PeopleFed { get; set; }
        public ICollection<RecipeIngredient> Ingredients { get; set; } = new List<RecipeIngredient>();
    }
}