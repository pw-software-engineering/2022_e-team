using System;
using System.Collections.Generic;
using System.Text;

namespace ECaterer.Contracts.Client
{
    public class ClientModel
    {
        public string Name { get; set; }
        public  string LastName { get; set; }
        public string Email { get; set; }
        public AddressModel Address { get; set; }
        public string PhoneNumber { get; set; }
    }
}
