using System;
using System.Collections.Generic;
using System.Text;

namespace ECaterer.Contracts.Orders
{
    public class ComplaintModel
    {
        public string ComplaintId { get; set; }
        public OrderClientModel Order { get; set; }
        public string Description { get; set; }
        public string? Answer { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
    }
}
