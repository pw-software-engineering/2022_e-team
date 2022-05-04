using ECaterer.Contracts.Orders;
using ECaterer.Web.DTO;

namespace ECaterer.Web.Converters
{
    public static class DietConverter
    {
        public static DietDTO ConvertBack(DietModel input)
        {
            return new DietDTO()
            {
                Id = input.DietId,
                Name = input.Name,
                Price = input.Price,
                Calories = input.Meals.Calories,
                Vegan = input.Vegan,
            };
        }
    }
}
