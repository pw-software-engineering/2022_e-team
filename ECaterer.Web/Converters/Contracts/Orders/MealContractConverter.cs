using ECaterer.Contracts.Orders;
using System;
using System.Collections.Generic;
using System.Text;
using ECaterer.Core.Models;
using System.Linq;

namespace ECaterer.Contracts.Converters.Orders
{
    public static class MealContractConverter
    {
        public static MealModel ConvertToContract(Meal meal)
        {
            return new MealModel
            {
                MealId = meal.MealId.ToString(),
                Name = meal.Name,
                Calories = meal.Calories,
                Vegan = meal.Vegan,
                AllergentList = meal.AllergentList.Select(a => a.Name).ToArray(),
                IngredientList = meal.IngredientList.Select(a => a.Name).ToArray(),
            };
        }

        public static Meal ConvertBack(MealModel meal)
        {
            return new Meal
            {
                MealId = Int32.Parse(meal.MealId),
                Name = meal.Name,
                Calories = meal.Calories,
                Vegan = meal.Vegan,
                AllergentList = meal.AllergentList.Select(a => new Allergent() { Name = a }).ToArray(),
                IngredientList = meal.IngredientList.Select(a => new Ingredient() { Name = a }).ToArray(),
            };
        }
    }
}
