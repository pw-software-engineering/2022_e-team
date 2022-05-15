using ECaterer.Contracts.Orders;
using ECaterer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECaterer.WebApi.Common.Interfaces
{
    public interface IOrdersService
    {
        Task<OrderListItemModel[]> GetOrders(GetOrdersQueryModel getOrdersQuery);
        Task<Order> AddOrder(AddOrderModel addOrderModel);
        Task<(bool exists, bool paid)> PayOrder(string orderId);
    }
}
