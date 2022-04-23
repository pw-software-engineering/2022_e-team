using ECaterer.Core.Models;
using ECaterer.Web.DTO.MealsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECaterer.WebApi.Common.Interfaces
{
    public interface IMealRepository : IDisposable
    {
        Task<IEnumerable<Meal>> GetMeals();
        Task<Meal> GetMealById(int mealId);
        void AddMeal(AddMealDTO mealDTO);
        void EditMeal(EditMealDTO mealDTO);
        void DeleteMeal(int mealId);
    }
}
