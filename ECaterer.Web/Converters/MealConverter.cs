using ECaterer.Contracts.Meals;
using ECaterer.Web.DTO.MealsDTO;

namespace ECaterer.Web.Converters
{
    public static class MealConverter
    {
        public static MealDTO ConvertBack(GetMealsResponseModel input)
        {
            return new MealDTO()
            {
                // AllergentList, IngredientList
                Id = input.Id,
                Calories = input.Calories,
                Name = input.Name,
                Vegan = input.Vegan,
            };
        } 
    }
}
