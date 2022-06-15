using ECaterer.Contracts.Orders;
using ECaterer.Web.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECaterer.Web.Converters
{
    public static class OrderConverter
    {
        public static ClientOrdersDTO ConvertBack(OrderClientModel input)
        {
            return new ClientOrdersDTO()
            {
                OrderNumber = "1",
                StartDate = input.StartDate,
                EndDate = input.EndDate,
                Status = input.Status,
                Price = input.Price,
                ComplaintStatus = input.Complaint.Status
            };
        }
    }
}
