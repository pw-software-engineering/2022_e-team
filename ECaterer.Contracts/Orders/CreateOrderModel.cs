using System;
using System.Collections.Generic;
using System.Text;

namespace ECaterer.Contracts.Orders
{
    public class CreateOrderModel
    {
        public ICollection<string> DietIDs { get; set; }
        public DeliveryDetailsModel DeliveryDetails { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

    }
}
