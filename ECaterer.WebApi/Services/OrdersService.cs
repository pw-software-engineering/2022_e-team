using ECaterer.Contracts.Client;
using ECaterer.Contracts.Orders;
using ECaterer.Core;
using ECaterer.Core.Models;
using ECaterer.Core.Models.Enums;
using ECaterer.WebApi.Common.Builders;
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

        public async Task<Order> AddOrder(string userId, AddOrderModel model)
        {
            if (model.EndDate <= model.StartDate || model.StartDate <= DateTime.Now)
                return null;

            if (ValidateDietsIDs(model.DietIDs))
                return null;

            var address = await GetDeliveryAddress(userId, model);
            var containedDiets = _context.Diets.Where(d => model.DietIDs.Contains(d.DietId)).ToList();
            var deliveryDetails = new DeliveryDetails()
            {
                Address = address,
                CommentForDeliverer = model.DeliveryDetails.CommentForDeliverer,
                PhoneNumber = model.DeliveryDetails.PhoneNumber,
            };
            var order = new Order
            {
                Status = (int)OrderStatus.WaitingForPayment,
                DeliveryDetails = new DeliveryDetails() 
                {
                    Address = address,
                    CommentForDeliverer = model.DeliveryDetails.CommentForDeliverer,
                    PhoneNumber = model.DeliveryDetails.PhoneNumber,
                },
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                Price = 0, //problem obliczenia ceny - dodac cene do Diet? - potrzebuje naprawienia /api/diets
                Diets = new List<Diet>(containedDiets)
            };

            _context.DeliveryDetails.Add(deliveryDetails);
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return order;
        }

        public async Task<IEnumerable<Order>> GetOrders(GetOrdersQueryModel query)
        {
            IQueryable<Order> orders; 
            try 
            {
                orders = _context.Orders
                    .Include(o => o.DeliveryDetails)
                    .Include(o => o.Complaint)
                    .Include(o => o.DeliveryDetails.Address);

                var builder = new OrdersQueryBuilder(orders);
                builder = builder
                    .SetPriceFilter(query.Price)
                    .SetPriceLowerThanFilter(query.Price_lt)
                    .SetPriceHigherThanFilter(query.Price_ht)
                    .SetStartDateFilter(query.StartDate)
                    .SetEndDateFilter(query.EndDate)
                    .SetStatusFilter(query.OrderStatus)
                    .SetSorting(query.Sort)
                    .SetOffset(query.Offset)
                    .SetLimit(query.Limit);

                return builder.GetQuery();
            }
            catch
            {
                return null;
            }
        }

        public async Task<(bool exists, bool paid)> PayOrder(string orderId)
        {
            var order = await _context.Orders
               .Where(o => o.Status == (int)OrderStatus.WaitingForPayment)
               .FirstOrDefaultAsync(o => o.OrderId == orderId);
            //sprawdzenie klienta - dodanie ClientId do Order?
            if (order == default)
                return (exists: false, paid: false);
            //przekazanie do zewnętrznego serwisu platnosci
            order.Status = (int)OrderStatus.Paid;
            await _context.SaveChangesAsync();

            return (exists: true, paid: true);
        }

        private bool ValidateDietsIDs(string[] dietIDs)
        {
            return dietIDs.Any(id => _context.Diets.Any(d => d.DietId == id)) /*|| dietIDs.Length == 0*/;
        }

        private async Task<Address> GetDeliveryAddress(string userId, AddOrderModel model)
        {
            Address address;
            if (model.DeliveryDetails.Address == null)
            {
                address = await GetUserAddress(userId);
            }
            else
            {
                address = await SearchForAddressInDatabase(model.DeliveryDetails.Address);
                if (address == null)
                {
                    address = new Address()
                    {
                        ApartmentNumber = model.DeliveryDetails.Address.ApartmentNumber,
                        BuildingNumber = model.DeliveryDetails.Address.BuildingNumber,
                        City = model.DeliveryDetails.Address.City,
                        PostCode = model.DeliveryDetails.Address.PostCode,
                        Street = model.DeliveryDetails.Address.Street
                    };
                    _context.Addresses.Add(address);
                }
            }
            return address;
        }

        private async Task<Address> GetUserAddress(string userId)
        {
            return await _context.Addresses
                .FirstOrDefaultAsync(a => a.AddressId == _context.Clients.First(c => c.ClientId == userId).Address.AddressId);
        }

        private async Task<Address> SearchForAddressInDatabase(AddressModel model)
        {
            return await _context.Addresses.FirstOrDefaultAsync(a =>
                a.ApartmentNumber == model.ApartmentNumber &&
                a.BuildingNumber == model.BuildingNumber &&
                a.Street == model.Street &&
                a.PostCode == model.PostCode &&
                a.City == model.City
            );
        }
    }
}
