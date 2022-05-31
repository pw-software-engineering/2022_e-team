using ECaterer.Contracts.Meals;
using ECaterer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECaterer.WebApi.Common.Interfaces
{
    public interface IMealRepository : IDisposable
    {
        Task<IEnumerable<Meal>> GetMeals(GetMealsQueryModel getMealsQuery);
        Task<Meal> GetMealById(string mealId);
        Task<Meal> AddMeal(MealModel mealModel);
        Task<Meal> EditMeal(string mealId, MealModel mealModel);
        Task<Meal> DeleteMeal(string mealId);
    }
}
