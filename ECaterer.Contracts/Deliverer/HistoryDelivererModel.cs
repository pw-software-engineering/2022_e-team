using ECaterer.Contracts.Orders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECaterer.Contracts.Deliverer
{
    public class HistoryDelivererModel
    {
        public string Id { get; set; }

        public HistoryDeliveryDetailsModel DeliveryDetails { get; set; }
    }
}
