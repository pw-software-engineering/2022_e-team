using ECaterer.Core.Models.Enums;
using ECaterer.WebApi.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ECaterer.WebApi.Common.Filters.Orders
{
    public class OrderMultiStatusFilter : IFilter<Core.Models.Order>
    {
        public List<OrderStatus> Statuses { get; private set; }

        public OrderMultiStatusFilter(IEnumerable<string> statuses)
        {
            this.Statuses = statuses.Select(s => Enum.Parse <OrderStatus>(s)).ToList();
        }

        public IQueryable<Core.Models.Order> Filter(IQueryable<Core.Models.Order> data)
        {
            if (Statuses is null)
                return data;
            return data.Where(o => Statuses.Contains((OrderStatus)o.Status));
        }
    }
}
