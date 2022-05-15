using ECaterer.Contracts.Orders;
using ECaterer.Core;
using ECaterer.Core.Models;
using ECaterer.WebApi.Common.Enums;
using ECaterer.WebApi.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECaterer.WebApi.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly DataContext _context;

        public OrdersService(DataContext context)
        {
            _context = context;
        }

        public async Task<Order> AddOrder(AddOrderModel addOrderModel)
        {
            throw new NotImplementedException();
        }

        public async Task<OrderListItemModel[]> GetOrders(GetOrdersQueryModel getOrdersQuery)
        {
            throw new NotImplementedException();
        }

        public async Task<(bool exists, bool paid)> PayOrder(string orderId)
        {
            var order = await _context.Orders
               .Where(o => o.Status.OrderStatusValue == OrderStatus.WaitingForPayment.ToString())
               .FirstOrDefaultAsync(o => o.OrderId == orderId);
            //sprawdzenie klienta - dodanie ClientId do Order?
            if (order == default)
                return (exists: false, paid: false);
            //przekazanie do zewnętrznego serwisu platnosci
            order.Status.OrderStatusValue = OrderStatus.Paid.ToString();
            await _context.SaveChangesAsync();

            return (exists: true, paid: true);
        }
    }
}
