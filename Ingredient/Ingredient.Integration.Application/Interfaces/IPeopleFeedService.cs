using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ingredient.Integration.Domain.Entities;

namespace Ingredient.Integration.Application.Interfaces
{
    public interface IPeopleFeedService
    {
        public Task<List<(string RecipeName, int Quantity)>> PeopleFeed(List<Recipe> recipes);
    }
}