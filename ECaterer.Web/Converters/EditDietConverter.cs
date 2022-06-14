using ECaterer.Contracts.Diets;
using ECaterer.Web.DTO;
using System.Linq;

namespace ECaterer.Web.Converters
{
    public static class EditDietConverter
    {
        public static EditDietDTO ConvertBack(DietModel input)
        {
            return new EditDietDTO()
            {
                Id = input.Id,
                Name = input.Name,
                Price = input.Price,
                Calories = input.Meals.Sum(meal => meal.Calories),
                Description = input.Description,
                Vegan = input.Vegan,
            };
        }
    }
}
