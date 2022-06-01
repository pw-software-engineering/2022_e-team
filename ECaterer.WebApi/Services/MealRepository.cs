using ECaterer.Contracts.Meals;
using ECaterer.Core;
using ECaterer.Core.Models;
using ECaterer.WebApi.Common.Builders;
using ECaterer.WebApi.Common.Exceptions;
using ECaterer.WebApi.Common.Filters;
using ECaterer.WebApi.Common.Filters.Meals;
using ECaterer.WebApi.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECaterer.WebApi.Services
{
    public class MealRepository : IMealRepository
    {
        private readonly DataContext _context;
        private bool disposed = false;

        public MealRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Meal> AddMeal(MealModel mealModel)
        {
            var ingredients = mealModel.IngredientList
                .Select(i => new Ingredient() { Name = i })
                .ToList();
            var allergents = mealModel.AllergentList
                .Select(a => new Allergent() { Name = a })
                .ToList();

            var meal = new Meal()
            {
                Name = mealModel.Name,
                Calories = mealModel.Calories,
                IngredientList = ingredients,
                AllergentList = allergents,
                Vegan = mealModel.Vegan
            };

            _context.Meals.Add(meal);
            _context.Ingredients.AddRange(ingredients);
            _context.Allergents.AddRange(allergents);
            await _context.SaveChangesAsync();

            return meal;
        }

        public async Task<Meal> DeleteMeal(string mealId)
        {
            var meal = await _context.Meals.FirstOrDefaultAsync(meal => meal.MealId == mealId);
            if (meal is null)
                return null;
            var containedByDiet = await _context.Diets.AnyAsync(diet => diet.Meals.Any(meal => meal.MealId == mealId));
            if (containedByDiet)
                throw new MealToRemoveIsContainedByDietException(mealId);
            var ingredientsToRemove = _context.Ingredients.Where(i => i.MealId == meal.MealId);
            var allergentsToRemove = _context.Allergents.Where(a => a.MealId == meal.MealId);
            _context.Ingredients.RemoveRange(ingredientsToRemove);
            _context.Allergents.RemoveRange(allergentsToRemove);
            _context.Meals.Remove(meal);
            await _context.SaveChangesAsync();

            return meal;
        }

        public async Task<Meal> EditMeal(string mealId, MealModel mealModel)
        {
            var meals = _context.Meals.Include(m => m.AllergentList).Include(m => m.IngredientList);
            var meal = await meals.FirstOrDefaultAsync(meal => meal.MealId == mealId);
            if (meal is null)
                return null;

            var ingredients = mealModel.IngredientList
                .Select(i => new Ingredient() { Name = i})
                .ToList();
            var allergents = mealModel.AllergentList
                .Select(a => new Allergent() { Name = a })
                .ToList();
            var oldIngredients = meal.IngredientList;
            var oldAllergents = meal.AllergentList;

            meal.Name = mealModel.Name;
            meal.IngredientList = ingredients;
            meal.AllergentList = allergents;
            meal.Name = mealModel.Name;
            meal.Calories = mealModel.Calories;
            meal.Vegan = mealModel.Vegan;

            _context.Meals.Update(meal);

            _context.Ingredients.AddRange(ingredients);
            _context.Allergents.AddRange(allergents);
            _context.Ingredients.RemoveRange(oldIngredients);
            _context.Allergents.RemoveRange(oldAllergents);
            await _context.SaveChangesAsync();

            return meal;
        }

        public async Task<Meal> GetMealById(string mealId)
        {
            var meals = _context.Meals.Include(m => m.AllergentList).Include(m => m.IngredientList);
            var meal = await meals.FirstOrDefaultAsync(meal => meal.MealId == mealId);

            return meal;
        }

        public async Task<IEnumerable<Meal>> GetMeals(GetMealsQueryModel query)
        {
            IQueryable<Meal> meals =  _context.Meals;
            var builder = new QueryBuilder<Meal>(meals);
            builder = builder
                .PerformAction(new MealNameFilter(query.Name))
                .PerformAction(new MealNameWithFilter(query.Name_with))
                .PerformAction(new MealCaloriesFilter(query.Calories))
                .PerformAction(new MealCaloriesLowerThanFilter(query.Calories_lt))
                .PerformAction(new MealCaloriesHigherThanFilter(query.Calories_ht))
                .PerformAction(new MealVeganFilter(query.Vegan))
                .PerformAction(new MealSort(query.Sort))
                .PerformAction(new Offset<Meal>(query.Offset))
                .PerformAction(new Limit<Meal>(query.Limit));
                
            return builder.GetQuery();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }
    }
}
