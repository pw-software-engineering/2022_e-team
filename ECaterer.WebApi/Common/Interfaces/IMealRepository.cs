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
        Task<IEnumerable<Meal>> GetMeals(int? offset, int? limit, string sort, string name, string name_with, bool? vegan, int? calories, int? colories_lt, int? colories_ht);
        Task<Meal> GetMealById(string mealId);
        Task<Meal> AddMeal(MealModel mealModel);
        Task<Meal> EditMeal(string mealId, MealModel mealModel);
        Task<Meal> DeleteMeal(string mealId);
    }
}
