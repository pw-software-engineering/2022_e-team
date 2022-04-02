using System;
using System.Collections.Generic;
using ECaterer.Core;
using FluentAssertions;
using Xunit;
using Microsoft.EntityFrameworkCore;
using ECaterer.Core.Models;
using ECaterer.Web.Controllers;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ECaterer.Web.DTO.ClientDTO;

namespace ECareter.Web.Test.ApiUnitTests
{
    public class ClientTests : IClassFixture<ClientDataSeedFixture>
    {
        ClientDataSeedFixture _fixture;

        public ClientTests(ClientDataSeedFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void Login_Tests()
        {

        }

        [Fact]
        public void Register_Tests()
        {
            
        }

        [Fact]
        public void Account_Tests()
        {

        }

        [Fact]
        public void GetOrdersWithoutParams_ReturnsArrayOfAllOrders()
        {
            var _controller = new ClientController(_fixture.context);

            var result = _controller.GetOrders().Result;
            var okResult = result.Result as OkResult;
            var ordersCount = result.Value is not null ? result.Value.Length : 0;

            okResult.Should().NotBeNull();
            ordersCount.Should().Be(3);
        }

        [Fact]
        public void PostOrder_ShouldAddOrderToDatabaseAndReturnOk()
        {
            var _controller = new ClientController(_fixture.context);
            var orderDTO = new OrderDTO()
            {
                dietIDs = new string[2] {"1", "2"},
                deliveryDetails = new DeliveryDetails()
                {
                    DeliveryDetailsId = 1,
                    Address = new Address()
                    {
                        AddressId = 1,
                        Street = "Koszykowa",
                        BuildingNumber = "75",
                        PostCode = "00-623",
                        City = "Warszawa"
                    },
                    PhoneNumber = "+48123456789"
                },
                startDate = DateTime.Now,
                endDate = DateTime.UtcNow
            };

            var okResult = _controller.PostOrder(orderDTO).Result as OkResult;
            var ordersCount = _fixture.context.Orders.Count();
            var last = _fixture.context.Orders.Last();
            if (last.OrderId != 3)
            {
                _fixture.context.Orders.Remove(last);
                _fixture.context.SaveChanges();
            }


            okResult.Should().NotBeNull();
            ordersCount.Should().Be(4);
        }

        [Fact]
        public void PostComplaint_ShouldAddComplaintToDatabaseAndToOrderAndReturnOk()
        {
            var _controller = new ClientController(_fixture.context);
            var complaintDTO = new ComplaintDTO()
            {
                complaint_description = "Some description" 
            };

            var okResult = _controller.PostComplaint(1, complaintDTO).Result as OkResult;
            var complaintsCount = _fixture.context.Complaints.Count();
            var complaint = _fixture.context.Orders.Find(1).Complaint;
            var addedComplaintToOrder = (complaint is not null && complaint.Description.Equals(complaintDTO.complaint_description));

            okResult.Should().NotBeNull();
            complaintsCount.Should().Be(1);
            addedComplaintToOrder.Should().BeTrue();
        }

        [Fact]
        public void PayOrderWithId_ShouldReturnOk()
        {
            var _controller = new ClientController(_fixture.context);

            var okResult = _controller.PayOrder(1).Result as OkResult;

            okResult.Should().NotBeNull();
        }

        [Fact]
        public void PayOrderWithUnexistingId_ShouldReturnBadRequest()
        {
            var _controller = new ClientController(_fixture.context);

            var badRequestResult = _controller.PayOrder(4).Result as BadRequestResult;

            badRequestResult.Should().NotBeNull();
        }
    }

    public class ClientDataSeedFixture : IDisposable
    {
        public DataContext context { get; private set; }
        private readonly DbContextOptions<DataContext> options;

        public ClientDataSeedFixture()
        {
            options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "ECatererDatabase")
                .Options;
            context = new DataContext(options);
            context.Orders.AddRange(GetSampleOrders());
            context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }

        private IEnumerable<Order> GetSampleOrders()
        {
            var orders = new List<Order>();
            var address1 = new Address()
            {
                AddressId = 1,
                Street = "Street 1",
                BuildingNumber = "1",
                ApartmentNumber = "1",
                PostCode = "00-250",
                City = "Warsaw"
            };
            var address2 = new Address()
            {
                AddressId = 2,
                Street = "Street 2",
                BuildingNumber = "2",
                ApartmentNumber = "2",
                PostCode = "00-520",
                City = "Krakow"
            };
            var deliveryDetail1 = new DeliveryDetails()
            {
                DeliveryDetailsId = 1,
                Address = address1,
                PhoneNumber = "+48123456789"
            };
            var deliveryDetail2 = new DeliveryDetails()
            {
                DeliveryDetailsId = 2,
                Address = address2,
                PhoneNumber = "+48987654321"
            };
            var order1 = new Order()
            {
                OrderId = 1,
                Diets = new List<Diet>(),
                DeliveryDetails = deliveryDetail1,
                StartDate = DateTime.Now,
                EndDate = DateTime.UtcNow,
                Price = 300.0m,
            };
            var order2 = new Order()
            {
                OrderId = 2,
                Diets = new List<Diet>(),
                DeliveryDetails = deliveryDetail2,
                StartDate = DateTime.Now,
                EndDate = DateTime.UtcNow,
                Price = 600.0m,
            };
            var order3 = new Order()
            {
                OrderId = 3,
                Diets = new List<Diet>(),
                DeliveryDetails = deliveryDetail1,
                StartDate = DateTime.Now,
                EndDate = DateTime.UtcNow,
                Price = 500.0m,
            };
            orders.Add(order1);
            orders.Add(order2);
            orders.Add(order3);

            return orders;
        }
    }
}
