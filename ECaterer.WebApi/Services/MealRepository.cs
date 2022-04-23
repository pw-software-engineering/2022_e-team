using ECaterer.Core;
using ECaterer.Core.Models;
using ECaterer.Web.DTO.MealsDTO;
using ECaterer.WebApi.Common.Exceptions;
using ECaterer.WebApi.Common.Interfaces;
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

        public async void AddMeal(AddMealDTO mealDTO)
        {
            throw new NotImplementedException();
        }

        public async void DeleteMeal(int mealId)
        {
            var meal = _context.Meals.FirstOrDefault(meal => meal.MealId == mealId);
            if (meal is null)
                throw new UnexistingMealException(mealId);
            _context.Meals.Remove(meal);
            await _context.SaveChangesAsync();
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

        public void EditMeal(EditMealDTO mealDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<Meal> GetMealById(int mealId)
        {
            var meal = _context.Meals.FirstOrDefault(meal => meal.MealId == mealId);
            if (meal is null)
                throw new UnexistingMealException(mealId);
            return meal;
        }

        public async Task<IEnumerable<Meal>> GetMeals()
        {
            throw new NotImplementedException();
        }
    }
}
