using ECaterer.Core.Models.Enums;
using ECaterer.WebApi.Common.Interfaces;
using System;
using System.Linq;

namespace ECaterer.WebApi.Common.Filters.Orders
{
    public class OrderStatusFilter : IFilter<Core.Models.Order>
    {
        public OrderStatus? status { get; private set; }

        public OrderStatusFilter(string status)
        {
            this.status = status is null ? null : Enum.Parse<OrderStatus>(status);
        }

        public IQueryable<Core.Models.Order> Filter(IQueryable<Core.Models.Order> data)
        {
            if (status is null)
                return data;
            return data.Where(o => o.Status == (int)status);
        }
    }
}
