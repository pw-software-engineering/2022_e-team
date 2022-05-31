using System;
using System.Collections.Generic;
using System.Text;

namespace ECaterer.Contracts.Producer
{
    public class GetOrdersProducerQueryModel
    {
        public int? Offset { get; set; }
        public int? Limit { get; set; }
        public string Sort { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
