using System;
using System.Collections.Generic;
using System.Text;

namespace ECaterer.Contracts.Orders
{
    public class GetOrdersQueryModel
    {
        public int? Offset { get; set; }
        public int? Limit { get; set; }
        public string Sort { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? Price { get; set; }
        public int? Price_lt { get; set; }
        public int? Price_ht { get; set; }
        public string OrderStatus { get; set; }
    }
}
