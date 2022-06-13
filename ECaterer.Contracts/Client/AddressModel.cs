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
        public string City { get; set; }

        public static string Stringify(AddressModel a)
        {
            if (string.IsNullOrEmpty(a.ApartmentNumber))
            {
                return string.Format("{0} {1}, {2} {3}", a.Street, a.BuildingNumber, a.PostCode, a.City);
            }
            else
            {
                return string.Format("{0} {1}/{2}, {3} {4}", a.Street, a.BuildingNumber, a.ApartmentNumber, a.PostCode, a.City);
            }
        }
    }
}
