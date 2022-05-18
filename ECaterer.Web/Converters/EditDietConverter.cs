using ECaterer.Contracts.Orders;
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
                Description = input.Description,
                Calories = input.Meals.Calories,
                Vegan = input.Vegan
            };
        }
    }
}
