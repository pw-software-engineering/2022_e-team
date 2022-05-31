using ECaterer.Web.DTO.ClientDTO;
using System;
using System.Collections.Generic;

namespace ECaterer.Web.DTO
{
    public class ProducerOrderDTO
    {
        public string OrderNumber { get; set; }

        public DateTime OrderDate { get; set; }

        public string Status { get; set; }

    }
}
