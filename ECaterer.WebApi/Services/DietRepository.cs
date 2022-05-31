﻿using ECaterer.Contracts.Converters.Orders;
using ECaterer.Contracts.Diets;
using ECaterer.Core;
using ECaterer.Core.Models;
using ECaterer.Core.Models.Enums;
using ECaterer.WebApi.Common.Exceptions;
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
    public class DietRepository: IDietRepository
    {
        DataContext _context;
        private bool disposed;

        public DietRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Diet> GetDietById(string dietId)
        {
            var diet = await _context.Diets.Include(d => d.Meals).FirstOrDefaultAsync(diet => diet.DietId == dietId);

            if (diet == default)
                return null;

            return diet;
        }

        public async Task<IEnumerable<Diet>> GetDiets(GetDietsQueryModel query)
        {
            return await _context.Diets.Include(d => d.Meals).ToListAsync();
        }


        public async Task<Diet> AddDiet(DietModel dietModel)
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

        public async Task<Diet> EditDiet(string dietId, DietModel dietModel)
        {
            var diet = await _context.Diets.FirstOrDefaultAsync(diet => diet.DietId == dietId);

            if (diet is null)
                return null;

            diet.Title = dietModel.Name;
            diet.Price = dietModel.Price;

            var meals = _context.Meals.Where(m => dietModel.MealIds.Contains(m.MealId.ToString())).ToArray();
            diet.Vegan = IsVegan(meals);
            diet.Calories = CalculateCalories(meals);
            diet.Meals = meals;

            _context.Diets.Update(diet);
            await _context.SaveChangesAsync();

            return diet;
        }

        public async Task<Diet> DeleteDiet(string dietId)
        {
            var diet = await _context.Diets.FirstOrDefaultAsync(diet => diet.DietId == dietId);

            if (diet is null)
                return null;

            var ordersWithDiet = _context.Orders.Include(d => d.Diets).Where(o => o.Diets.Any(d => d.DietId == dietId));
            await ordersWithDiet.ForEachAsync(o => o.Status = (int)OrderStatus.Canceled);
           
            _context.Diets.Remove(diet);
            await _context.SaveChangesAsync();

            return diet;
        }

        private IEnumerable<Meal> GetMeals(DietModel dietModel) => _context.Meals.Where(m => dietModel.MealIds.Contains(m.MealId)).ToList();

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