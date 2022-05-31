using ECaterer.Core.Models.Enums;
using ECaterer.WebApi.Common.Interfaces;
using NodaTime;
using System;
using System.Linq;

namespace ECaterer.WebApi.Common.Filters.Orders
{
    public class OrderStartDateFilter : IFilter<Core.Models.Order>
    {
        public DateTime? startDate { get; private set; }

        public OrderStartDateFilter(DateTime? startDate)
        {
            this.startDate = startDate;
        }

        public IQueryable<Core.Models.Order> Filter(IQueryable<Core.Models.Order> data)
        {
            if (startDate is null)
                return data;
            return data.Where(o => Period.Between(LocalDate.FromDateTime((DateTime)startDate), LocalDate.FromDateTime(o.StartDate)).Days == 0);
        }
    }
}
