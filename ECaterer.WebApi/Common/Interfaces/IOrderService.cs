using ECaterer.Contracts.Orders;
using ECaterer.Contracts.Producer;
using ECaterer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECaterer.WebApi.Common.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetOrders(GetOrdersClientQueryModel getOrdersQuery);
        Task<IEnumerable<Order>> GetOrders(GetOrdersProducerQueryModel getOrdersQuery);
        Task<Order> AddOrder(string userId, AddOrderModel addOrderModel);
        Task<(bool exists, bool paid)> PayOrder(string orderId);
        Task<(bool exists, bool completed)> CompleteOrder(string orderId);
    }
}
