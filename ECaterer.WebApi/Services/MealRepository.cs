using ECaterer.Core;
using ECaterer.Core.Models;
using ECaterer.Web.DTO.MealsDTO;
using ECaterer.WebApi.Common.Exceptions;
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

        public async Task<Meal> AddMeal(MealDTO mealDTO)
        {
            var ingredients = mealDTO.IngredientList
                .Select(i => new Ingredient() { Name = i })
                .ToList();
            var allergents = mealDTO.AllergentList
                .Select(a => new Allergent() { Name = a })
                .ToList();

            var meal = new Meal()
            {
                Name = mealDTO.Name,
                Calories = mealDTO.Calories,
                IngredientList = ingredients,
                AllergentList = allergents,
                Vegan = mealDTO.Vegan
            };

            _context.Meals.Add(meal);
            _context.Ingredients.AddRange(ingredients);
            _context.Allergents.AddRange(allergents);
            await _context.SaveChangesAsync();

            return meal;
        }

        public async Task<Meal> DeleteMeal(int mealId)
        {
            var meal = await _context.Meals.FirstOrDefaultAsync(meal => meal.MealId == mealId);
            if (meal is null)
                throw new UnexistingMealException(mealId);
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

        public async Task<Meal> EditMeal(int mealId, MealDTO mealDTO)
        {
            var meals = _context.Meals.Include(m => m.AllergentList).Include(m => m.IngredientList);
            var meal = await meals.FirstOrDefaultAsync(meal => meal.MealId == mealId);
            if (meal is null)
                throw new UnexistingMealException(mealId);

            var ingredients = mealDTO.IngredientList
                .Select(i => new Ingredient() { Name = i})
                .ToList();
            var allergents = mealDTO.AllergentList
                .Select(a => new Allergent() { Name = a })
                .ToList();
            var oldIngredients = meal.IngredientList;
            var oldAllergents = meal.AllergentList;

            meal.Name = mealDTO.Name;
            meal.IngredientList = ingredients;
            meal.AllergentList = allergents;
            meal.Name = mealDTO.Name;
            meal.Vegan = mealDTO.Vegan;

            _context.Meals.Update(meal);

            _context.Ingredients.AddRange(ingredients);
            _context.Allergents.AddRange(allergents);
            _context.Ingredients.RemoveRange(oldIngredients);
            _context.Allergents.RemoveRange(oldAllergents);
            await _context.SaveChangesAsync();

            return meal;
        }

        public async Task<Meal> GetMealById(int mealId)
        {
            var meals = _context.Meals.Include(m => m.AllergentList).Include(m => m.IngredientList);
            var meal = await meals.FirstOrDefaultAsync(meal => meal.MealId == mealId);
            if (meal is null)
                throw new UnexistingMealException(mealId);
            return meal;
        }

        public async Task<IEnumerable<Meal>> GetMeals(int? offset, int? limit, string sort, string name, string name_with, bool? vegan, int? calories, int? colories_lt, int? colories_ht)
        {
            var meals = await _context.Meals.ToListAsync();
            if (sort is not null)
                switch(sort)
                {
                    case "title(asc)":
                        meals = meals.OrderBy(m => m.Name).ToList();
                        break;
                    case "title(desc)":
                        meals = meals.OrderByDescending(m => m.Name).ToList();
                        break;
                    case "calories(asc)":
                        meals = meals.OrderBy(m => m.Calories).ToList();
                        break;
                    case "calories(desc)":
                        meals = meals.OrderByDescending(m => m.Calories).ToList();
                        break;
                    default:
                        throw new Exception("Unexpected sort type");
                }
            if (name is not null)
                meals = meals.Where(m => m.Name.Equals(name)).ToList();
            if (name_with is not null)
                meals = meals.Where(m => m.Name.Contains(name_with)).ToList();
            if (vegan is not null)
                meals = meals.Where(m => m.Vegan == vegan).ToList();
            if (calories is not null)
                meals = meals.Where(m => m.Calories == calories).ToList();
            if (colories_lt is not null)
                meals = meals.Where(m => m.Calories <= colories_lt).ToList();
            if (colories_ht is not null)
                meals = meals.Where(m => m.Calories >= colories_ht).ToList();
            if (offset is not null)
                meals = meals.Skip((int)offset).ToList();
            if (limit is not null)
                meals = meals.Take((int)limit).ToList();
            return meals;
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
