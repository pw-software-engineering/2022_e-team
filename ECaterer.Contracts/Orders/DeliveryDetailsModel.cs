using ECaterer.Contracts.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECaterer.Contracts.Orders
{
    public class DeliveryDetailsModel
    {
        public AddressModel Address { get; set; }

        public string PhoneNumber { get; set; }

        public string CommentForDeliverer { get; set; }

    }
}
