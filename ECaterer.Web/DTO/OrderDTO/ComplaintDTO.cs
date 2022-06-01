using ECaterer.Web.DTO.ClientDTO;
using System;
using System.Collections.Generic;

namespace ECaterer.Web.DTO
{
    public class ComplaintOrderDTO
    {
        public string ClientName { get; set; }
        public string Status { get; set; }
        public DateTime ComplaintDate { get; set; }
        public string Description { get; set; }

    }
}
