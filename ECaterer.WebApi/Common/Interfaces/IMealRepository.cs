using ECaterer.Core.Models;
using ECaterer.Web.DTO.MealsDTO;
using ECaterer.WebApi.Common.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECaterer.WebApi.Common.Interfaces
{
    public interface IMealRepository : IDisposable
    {
        Task<IEnumerable<Meal>> GetMeals(int? offset, int? limit, string sort, string name, string name_with, bool? vegan, int? calories, int? colories_lt, int? colories_ht);
        Task<Meal> GetMealById(int mealId);
        Task<Meal> AddMeal(MealDTO mealDTO);
        Task<Meal> EditMeal(int mealId, MealDTO mealDTO);
        Task<Meal> DeleteMeal(int mealId);
    }
}
