using ECaterer.Web.DTO.ClientDTO;
using System;
using System.Collections.Generic;

namespace ECaterer.Web.DTO
{
    public class DelivererHistoryDTO
    {
        public string OrderNumber { get; set; }

        public string Comment { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public DateTime DeliveryDate { get; set; }
    }
}
