using ECaterer.Core.Models.Enums;
using ECaterer.WebApi.Common.Interfaces;
using NodaTime;
using System;
using System.Linq;

namespace ECaterer.WebApi.Common.Filters.Orders
{
    public class OrderSort : IFilter<Core.Models.Order>
    {
        public string sort { get; private set; }

        public OrderSort(string sort)
        {
            this.sort = sort;
        }

        public IQueryable<Core.Models.Order> Filter(IQueryable<Core.Models.Order> data)
        {
            IQueryable<Core.Models.Order> result = null;
            switch (sort)
            {
                case "startDate(asc)":
                    result = data.OrderBy(o => o.StartDate);
                    break;
                case "startDate(desc)":
                    result = data.OrderByDescending(o => o.StartDate);
                    break;
                case "endDate(asc)":
                    result = data.OrderBy(o => o.EndDate);
                    break;
                case "endDate(desc)":
                    result = data.OrderByDescending(o => o.EndDate);
                    break;
                case "orderId(asc)":
                    result = data.OrderBy(o => o.OrderId);
                    break;
                case "orderId(desc)":
                    result = data.OrderByDescending(o => o.OrderId);
                    break;
                case "price(asc)":
                    result = data.OrderBy(o => o.Price);
                    break;
                case "price(desc)":
                    result = data.OrderByDescending(o => o.Price);
                    break;
                case null:
                    return data;
                default:
                    throw new Exception("Unexpected sort type");
            }
            return result;
        }
    }
}
