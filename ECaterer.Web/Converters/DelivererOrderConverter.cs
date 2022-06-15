using ECaterer.Contracts.Deliverer;
using ECaterer.Web.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECaterer.Web.Converters
{
    public static class DelivererOrderConverter
    {
        public static DelivererOrderDTO ConvertBack(OrderDelivererModel dto)
        {
            return new DelivererOrderDTO
            {
              OrderNumber = dto.Id,
              Comment = dto.DeliveryDetails.CommentForDeliverer,
              Phone = dto.DeliveryDetails.PhoneNumber,
              Address = dto.DeliveryDetails.Address.ToString()
            };
        }
    }
}
