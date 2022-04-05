using ECaterer.Core.Models;
using System;

namespace ECaterer.Web.DTO.ClientDTO
{
    public class OrderDTO
    {
        public string[] dietIDs { get; set; }
        public DeliveryDetails deliveryDetails { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
    }
}
