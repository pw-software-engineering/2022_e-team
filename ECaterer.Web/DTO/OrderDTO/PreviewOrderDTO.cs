using ECaterer.Web.DTO.ClientDTO;
using System;
using System.Collections.Generic;

namespace ECaterer.Web.DTO
{
    public class PreviewOrderDTO
    {
        public string OrderNumber { get; set; }
        public IEnumerable<string> DietNames { get; set; }

        public IEnumerable<string> MealsConcatenated { get; set; }
        public decimal Cost { get; set; }
        public string Status { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime DeliverDate { get; set; }
        public string Comment { get; set; }
        public bool HasComplaint { get; set; }

    }
}
