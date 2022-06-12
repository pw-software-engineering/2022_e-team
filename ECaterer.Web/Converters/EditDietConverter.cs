using ECaterer.Contracts.Diets;
using ECaterer.Web.DTO;

namespace ECaterer.Web.Converters
{
    public static class EditDietConverter
    {
        public static EditDietDTO ConvertBack(DietModel input)
        {
            return new EditDietDTO()
            {
                Id = input.DietId,
                Calories = input.Calories,
                Description = input.Description,
                Vegan = input.Vegan,
            };
        }
    }
}
