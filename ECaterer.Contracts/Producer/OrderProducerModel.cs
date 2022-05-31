using ECaterer.Contracts.Orders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECaterer.Contracts.Producer
{
    public class OrderProducerModel
    {
        public string Id { get; set; }
        public string[] DietIDs { get; set; }
        public DeliveryDetailsModel DeliveryDetails { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
