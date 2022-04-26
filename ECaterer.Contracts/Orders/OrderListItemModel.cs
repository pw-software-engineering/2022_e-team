using System;
using System.Collections.Generic;
using System.Text;

namespace ECaterer.Contracts.Orders
{
    public class OrderListItemModel
    {
        public int Id { get; set; }

        public DateTimeOffset StartDate { get; set; }

        public DateTimeOffset EndDate { get; set; }

        public int Price { get; set; }

        public DietModel[] Diets { get; set; }

        public DeliveryDetailsModel DeliveryDetails { get; set; }

        public OrderStatusModel Status { get; set; }

        public ComplaintModel Complaint { get; set; }

    }
}
