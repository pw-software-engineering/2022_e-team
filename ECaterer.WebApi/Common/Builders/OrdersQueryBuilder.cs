using ECaterer.Core.Models;
using ECaterer.Core.Models.Enums;
using NodaTime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECaterer.WebApi.Common.Builders
{
    public class OrdersQueryBuilder
    {
        private IQueryable<Order> orders;

        public OrdersQueryBuilder(IQueryable<Order> orders)
        {
            this.orders = orders;
        }

        public OrdersQueryBuilder SetPriceFilter(int? price)
        {
            if (price is not null)
                orders = orders.Where(o => o.Price == price);
            return new OrdersQueryBuilder(orders);
        }

        public OrdersQueryBuilder SetPriceLowerThanFilter(int? price)
        {
            if (price is not null)
                orders = orders.Where(o => o.Price <= price);
            return new OrdersQueryBuilder(orders);
        }

        public OrdersQueryBuilder SetPriceHigherThanFilter(int? price)
        {
            if (price is not null)
                orders = orders.Where(o => o.Price >= price);
            return new OrdersQueryBuilder(orders);
        }

        public OrdersQueryBuilder SetStatusFilter(string? status)
        {
            if (status is not null)
                orders = orders.Where(o => o.Status == (int)Enum.Parse<OrderStatus>(status));
            return new OrdersQueryBuilder(orders);
        }

        public OrdersQueryBuilder SetStartDateFilter(DateTime? startDate)
        {
            if (startDate is not null)
                orders = orders.Where(o => Period.Between(LocalDate.FromDateTime((DateTime)startDate), LocalDate.FromDateTime(o.StartDate)).Days == 0);
            return new OrdersQueryBuilder(orders);
        }

        public OrdersQueryBuilder SetEndDateFilter(DateTime? startDate)
        {
            if (startDate is not null)
                orders = orders.Where(o => Period.Between(LocalDate.FromDateTime((DateTime)startDate), LocalDate.FromDateTime(o.StartDate)).Days == 0);
            return new OrdersQueryBuilder(orders);
        }

        public OrdersQueryBuilder SetSorting(string sort)
        {
            switch (sort)
            {
                case "startDate(asc)":
                    orders = orders.OrderBy(o => o.StartDate);
                    break;
                case "startDate(desc)":
                    orders = orders.OrderByDescending(o => o.StartDate);
                    break;
                case "endDate(asc)":
                    orders = orders.OrderBy(o => o.EndDate);
                    break;
                case "endDate(desc)":
                    orders = orders.OrderByDescending(o => o.EndDate);
                    break;
                case "orderId(asc)":
                    orders = orders.OrderBy(o => o.OrderId);
                    break;
                case "orderId(desc)":
                    orders = orders.OrderByDescending(o => o.OrderId);
                    break;
                case "price(asc)":
                    orders = orders.OrderBy(o => o.Price);
                    break;
                case "price(desc)":
                    orders = orders.OrderByDescending(o => o.Price);
                    break;
                case null:
                    break;
                default:
                    throw new Exception("Unexpected sort type");
            }
            return new OrdersQueryBuilder(orders);
        }

        public OrdersQueryBuilder SetOffset(int? offset)
        {
            if (offset is not null)
                orders = orders.Skip((int)offset);
            return new OrdersQueryBuilder(orders);
        }

        public OrdersQueryBuilder SetLimit(int? limit)
        {
            if (limit is not null)
                orders = orders.Take((int)limit);
            return new OrdersQueryBuilder(orders);
        }
    }
}
