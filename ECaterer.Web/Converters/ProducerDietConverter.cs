using ECaterer.Contracts.Orders;
using ECaterer.Web.DTO;

namespace ECaterer.Web.Converters
{
    public static class ProducerDietConverter
    {
        public static ProducerDietDTO ConvertBack(DietModel input)
        {
            return new ProducerDietDTO()
            {
                Id = input.DietId,
                Name = input.Name,
                Description = input.Description
            };
        }
    }
}
