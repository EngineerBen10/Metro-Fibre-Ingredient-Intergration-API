using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ingredient.Integration.Application.Interfaces;
using Ingredient.Integration.Application.Services;
using Ingredient.Integration.Domain.Entities;
using Ingredient.Integration.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ingredient.Integration.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PeopleFeedController : ControllerBase
    {
        private readonly IPeopleFeedService _peopleFeedService;
        private readonly AppDbContext _context;

        public PeopleFeedController(IPeopleFeedService peopleFeedService, AppDbContext context)
        {
            _peopleFeedService = peopleFeedService;
            _context = context;
        }

        [HttpGet("PeopleFeed")]

        public async Task<IActionResult> GetPeopleFeed()
        {
            var recipes = await _context.Recipes
                .Include(r => r.Ingredients)
                    .ThenInclude(ri => ri.Ingredient)
                .ToListAsync();

            var result = await _peopleFeedService.PeopleFeed(recipes);


                    return Ok(new
        {
            TotalPeopleFed = result.Sum(x => x.Quantity), // we assume only one person per recipe
            Recipes = result.Select(r => new
            {
                Recipe = r.RecipeName,
                Quantity = r.Quantity
            })
        });

        }
    }
}