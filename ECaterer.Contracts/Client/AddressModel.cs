using System;
using System.Collections.Generic;
using System.Text;

namespace ECaterer.Contracts.Client
{
    public class AddressModel
    {
        public string Street { get; set; }
        public string BuildingNumber { get; set; }
        public string ApartmentNumber { get; set; }
        public string PostCode { get; set; }
        public  string City { get; set; }
    }
}
