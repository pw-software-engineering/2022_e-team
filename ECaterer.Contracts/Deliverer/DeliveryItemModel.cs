using ECaterer.Contracts.Orders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECaterer.Contracts.Deliverer
{
    public class DeliveryItemModel
    {
        public int Id { get; set; }

        public DeliveryDetailsModel DeliveryDetails { get; set; }
    }
}
