using ECaterer.Contracts.Diets;
using ECaterer.Contracts.Orders;
using ECaterer.Web.DTO;
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

        public static Contracts.Diets.DietModel Convert(SaveDietDTO input)
        {
            return new Contracts.Diets.DietModel()
            {
                // TODO: need to add name and price
                MealIds = input.Meals.Select(meal => meal.Id).ToArray(),
            };
        }
    }
}
