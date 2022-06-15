using ECaterer.Contracts.Orders;
using ECaterer.Web.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECaterer.Web.Converters
{
    public static class ClientOrderConverter
    {
        public static PreviewOrderDTO ConvertBack(OrderClientModel dto)
        {
            return new PreviewOrderDTO
            {
                OrderNumber = dto.Id,
                DietNames = dto.DietIDs,
                Cost = dto.Price,
                Status = dto.Status,
                Address = dto.DeliveryDetails?.Address?.ToString(),
                Phone = dto.DeliveryDetails.PhoneNumber,
                OrderDate = dto.StartDate,
                DeliverDate = dto.EndDate,
                Comment = dto.DeliveryDetails.CommentForDeliverer,
                HasComplaint = dto.Complaint != null
            };
        }
    }
}
