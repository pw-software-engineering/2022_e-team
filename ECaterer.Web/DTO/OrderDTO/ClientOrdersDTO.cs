using ECaterer.Web.DTO.ClientDTO;
using System;
using System.Collections.Generic;

namespace ECaterer.Web.DTO
{
    public class ClientOrdersDTO
    {
        public string OrderNumber { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Status { get; set; }

        public decimal Price { get; set; }

        public string ComplaintStatus { get; set; }
    }
}
