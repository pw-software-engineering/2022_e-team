using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECaterer.Web.DTO.ClientDTO
{
    public class RegisterDTO
    {
        public string Password { get; set; }

        public ClientDTO Client { get; set; }

        public AddressDTO Address { get; set; }
    }
}
