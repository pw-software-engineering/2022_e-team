using ECaterer.Contracts.Orders;
using ECaterer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ECaterer.Contracts.Converters.Orders
{
    public static class DietConverter
    {
        public static DietModel ConvertToContract(Diet diet)
        {
            return new DietModel
            {
                DietId = diet.DietId.ToString(),
                Name = diet.Title,
                Description = diet.Description,
                Meals = diet.Meals.Select(m => MealContractConverter.ConvertToContract(m)).ToArray(),
                Price = diet.Price,
                Vegan = diet.Vegan,
                Calories = diet.Calories
            };
        }
    }
}
