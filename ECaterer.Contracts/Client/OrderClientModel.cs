using System;
using System.Collections.Generic;
using System.Text;

namespace ECaterer.Contracts.Orders
{
    public class OrderClientModel
    {
        public string Id { get; set; }
        public string[] DietIDs { get; set; }
        public DeliveryDetailsModel DeliveryDetails { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Price { get; set; }
        public string Status { get; set; }
        public ComplaintModel Complaint { get; set; }
    }
}
