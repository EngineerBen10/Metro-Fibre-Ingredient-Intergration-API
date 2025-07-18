using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Ingredient.Integration.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ingredient.Integration.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Ingredient.Integration.Domain.Entities.Ingredient> Ingredients => Set<Ingredient.Integration.Domain.Entities.Ingredient>();
        public DbSet<Recipe> Recipes => Set<Recipe>();
        public DbSet<RecipeIngredient> RecipeIngredient => Set<RecipeIngredient>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RecipeIngredient>()
            .HasKey(k => new { k.RecipeId, k.IngredientId });

            modelBuilder.Entity<RecipeIngredient>()
            .HasOne(k => k.Recipe)
            .WithMany(r => r.Ingredients)
            .HasForeignKey(k => k.RecipeId);

            modelBuilder.Entity<RecipeIngredient>()
            .HasOne(k => k.Ingredient)
            .WithMany(r => r.Recipes)
            .HasForeignKey(k => k.IngredientId);

            base.OnModelCreating(modelBuilder);
        }

    }
}