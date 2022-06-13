using ECaterer.Contracts.Client;
using ECaterer.Contracts.Deliverer;
using ECaterer.Contracts.Orders;
using ECaterer.Web.DTO;

namespace ECaterer.Web.Converters
{
    public static class HistoryDelivererConverter
    {
        public static DelivererHistoryDTO Convert(HistoryDelivererModel input)
        {
            return new DelivererHistoryDTO()
            {
                OrderNumber = input.Id,
                Address = AddressModel.Stringify(input.DeliveryDetails.Address),
                Comment = input.DeliveryDetails.CommentForDeliverer,
                Phone = input.DeliveryDetails.PhoneNumber,
                DeliveryDate = input.DeliveryDetails.DeliveryDate
            };
        }
    }
}
