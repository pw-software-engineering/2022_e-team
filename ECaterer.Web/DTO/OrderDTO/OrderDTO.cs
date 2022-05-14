using ECaterer.Web.DTO.ClientDTO;
using System;
using System.Collections.Generic;

namespace ECaterer.Web.DTO
{
    public class OrderDTO
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Comment { get; set; }

        public IEnumerable<string> DietIds { get; set; }

        public AddressDTO address { get; set; }
    }
}
