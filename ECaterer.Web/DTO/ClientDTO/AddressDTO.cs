using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECaterer.Web.DTO.ClientDTO
{
    public class AddressDTO
    {
        public string Street { get; set; }

        public string Building { get; set; }

        public string Apartment { get; set; }

        public string Code { get; set; }

        public string City { get; set; }
    }
}
