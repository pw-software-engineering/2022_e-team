using ECaterer.Contracts.Diets;
using ECaterer.Contracts.Orders;
using ECaterer.Web.DTO;
using System.Collections.Generic;
using System.Linq;

namespace ECaterer.Web.Converters
{
    public static class DietConverter
    {
        public static DietDTO ConvertBack(GetDietsModel input)
        {
            return new DietDTO()
            {
                Id = input.Id,
                Name = input.Name,
                Price = input.Price,
                Calories = input.Calories,
                Vegan = input.Vegan,
            };
        }

        public static AddEditDietModel Convert(SaveDietDTO input, List<string> mealIds)
        {
            return new AddEditDietModel()
            {
                Name = input.Name,
                Price = input.Price,
                MealIds = mealIds.ToArray(),
            };
        }
    }
}
