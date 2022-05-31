using ECaterer.WebApi.Common.Interfaces;
using NodaTime;
using System;
using System.Linq;

namespace ECaterer.WebApi.Common.Filters.Orders
{
    public class OrderEndDateFilter : IFilter<Core.Models.Order>
    {
        public DateTime? endDate { get; private set; }

        public OrderEndDateFilter(DateTime? endDate)
        {
            this.endDate = endDate;
        }

        public IQueryable<Core.Models.Order> Filter(IQueryable<Core.Models.Order> data)
        {
            if (endDate is null)
                return data;
            return data.Where(o => Period.Between(LocalDate.FromDateTime((DateTime)endDate), LocalDate.FromDateTime(o.EndDate)).Days == 0);
        }
    }
}
