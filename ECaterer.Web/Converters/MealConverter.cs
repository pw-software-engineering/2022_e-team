using ECaterer.Contracts.Meals;
using ECaterer.Web.DTO.MealsDTO;

namespace ECaterer.Web.Converters
{
    public static class MealConverter
    {
        public static MealDTO ConvertBack(MealModel input)
        {
            return new MealDTO()
            {                
                Id = input.MealId,
                AllergentList = input.AllergentList,
                IngredientList = input.IngredientList,
                Calories = input.Calories,
                Name = input.Name,
                Vegan = input.Vegan,
            };
        }

        public static AddMealModel Convert(MealDTO input)
        {
            return new AddMealModel()
            {
                Name = input.Name,
                Calories = input.Calories,
                Vegan = input.Vegan,
                AllergentList = input.AllergentList,
                IngredientList = input.IngredientList,
            };
        }
    }
}
