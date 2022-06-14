using ECaterer.Contracts.Diets;
using ECaterer.Contracts.Orders;
using ECaterer.Web.DTO;

namespace ECaterer.Web.Converters
{
    public static class ProducerDietConverter
    {
        public static ProducerDietDTO ConvertBack(GetDietsModel input)
        {
            return new ProducerDietDTO()
            {
                Id = input.Id,
                Name = input.Name,
            };
        }
    }
}
