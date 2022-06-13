using ECaterer.Contracts.Diets;
using ECaterer.Core;
using ECaterer.Core.Models;
using ECaterer.Core.Models.Enums;
using ECaterer.WebApi.Common.Builders;
using ECaterer.WebApi.Common.Exceptions;
using ECaterer.WebApi.Common.Filters;
using ECaterer.WebApi.Common.Filters.Diets;
using ECaterer.WebApi.Common.Interfaces;
using ECaterer.WebApi.Common.Queries;
using ECaterer.WebApi.Controllers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECaterer.WebApi.Services
{
    public class DietRepository : IDietRepository
    {
        DataContext _context;
        private bool disposed;

        public DietRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Diet> GetDietById(string dietId)
        {
            var diet = await _context.Diets
                .Include(d => d.Meals)
                .FirstOrDefaultAsync(diet => diet.DietId == dietId);

            if (diet == default)
                return null;

            var meals = diet.Meals.ToArray();
            for (int i = 0; i < meals.Length; i++)
                meals[i] = await _context.Meals
                    .Include(m => m.IngredientList)
                    .Include(m => m.AllergentList)
                    .FirstOrDefaultAsync(m => m.MealId == meals[i].MealId);

            return diet;
        }

        public async Task<IEnumerable<Diet>> GetDiets(GetDietsQueryModel query)
        {
            IQueryable<Diet> diets = _context.Diets;
            var builder = new QueryBuilder<Diet>(diets);
            builder = builder
                .PerformAction(new DietNameFilter(query.Name))
                .PerformAction(new DietNameWithFilter(query.Name_with))
                .PerformAction(new DietCaloriesFilter(query.Calories))
                .PerformAction(new DietCaloriesLowerThanFilter(query.Calories_lt))
                .PerformAction(new DietCaloriesHigherThanFilter(query.Calories_ht))
                .PerformAction(new DietPriceFilter(query.Price))
                .PerformAction(new DietPriceLowerThanFilter(query.Price_lt))
                .PerformAction(new DietPriceHigherThanFilter(query.Price_ht))
                .PerformAction(new DietVeganFilter(query.Vegan))
                .PerformAction(new DietSort(query.Sort))
                .PerformAction(new Offset<Diet>(query.Offset))
                .PerformAction(new Limit<Diet>(query.Limit));

            return builder.GetQuery();
        }


        public async Task<Diet> AddDiet(AddEditDietModel dietModel)
        {
            var meals = GetMeals(dietModel);

            var diet = new Diet
            {
                Title = dietModel.Name,
                Calories = CalculateCalories(meals),
                Description = string.Empty, //specification doesnt require description property in DietModel
                Vegan = IsVegan(meals),
                Price = dietModel.Price,
                Meals = meals.ToList()
            };

            _context.Diets.Add(diet);
            await _context.SaveChangesAsync();

            return diet;
        }

        public async Task<Diet> EditDiet(string dietId, AddEditDietModel dietModel)
        {
            var diet = await _context.Diets.FirstOrDefaultAsync(diet => diet.DietId == dietId);

            if (diet is null)
                return null;

            diet.Title = dietModel.Name;
            diet.Price = dietModel.Price;

            var meals = _context.Meals.Where(m => dietModel.MealIds.Contains(m.MealId)).ToArray();

            if (meals.Length != dietModel.MealIds.Length)
                return null;

            diet.Vegan = IsVegan(meals);
            diet.Calories = CalculateCalories(meals);
            diet.Meals = meals;

            _context.Diets.Update(diet);
            await _context.SaveChangesAsync();

            return diet;
        }

        public async Task<(bool exists, bool deleted)> DeleteDiet(string dietId)
        {
            var diet = await _context.Diets.FirstOrDefaultAsync(diet => diet.DietId == dietId);

            if (diet is null)
                return (false, false);

            var ordersWithDiet = _context.Orders.Include(d => d.Diets).Where(o => o.Diets.Any(d => d.DietId == dietId));
            var allowedStatuses = new List<OrderStatus>() { OrderStatus.Canceled, OrderStatus.Finished };

            await ordersWithDiet.ForEachAsync(o =>
            {
                o.Price -= diet.Price;
                o.Diets.Remove(diet);
            });
            var mealsWithDiet = _context.Meals.Where(m => m.DietId == dietId);
            await mealsWithDiet.ForEachAsync(m => m.DietId = null);
            _context.Diets.Remove(diet);
            await _context.SaveChangesAsync();

            return (true, true);
        }

        private IEnumerable<Meal> GetMeals(AddEditDietModel dietModel) => _context.Meals.Where(m => dietModel.MealIds.Contains(m.MealId)).ToList();

        private bool IsVegan(IEnumerable<Meal> meals) => meals.All(m => m.Vegan);

        private int CalculateCalories(IEnumerable<Meal> meals) => meals.Sum(m => m.Calories);

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
