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

        //[Fact]
        public void GetClientData_ShouldReturnClientDataAndOk()
        {
            var _controller = new ClientController(_fixture.context);
            var johnSmith = _fixture.context.Clients.FirstOrDefault();

            //login here

            var result = _controller.GetClientData().Result;
            var okResult = result.Result as OkResult;
            var clientData = result.Value;

            okResult.Should().NotBeNull();
            clientData.Should().Be(johnSmith);
        }

        //[Fact]
        public void EditClientData_ShouldReturnEditedClientDataAndOk()
        {
            var _controller = new ClientController(_fixture.context);
            var johnSmith = _fixture.context.Clients.Find(1);
            var tomLukas = _fixture.context.Clients.Find(2);


            var result = _controller.EditClientData(tomLukas);
            var okResult = result.Result as OkResult;
            var clientData = _fixture.context.Clients.FirstOrDefault();
            if (clientData.Name == "Tom")
            {
                _fixture.context.Clients.Remove(tomLukas);
                _fixture.context.Clients.Add(johnSmith);
                _fixture.context.SaveChanges();
            }

            okResult.Should().NotBeNull();
            clientData.Should().Be(tomLukas);
        }

        //[Fact]
        public void GetOrdersWithoutParams_ReturnsArrayOfAllOrders()
        {
            var _controller = new ClientController(_fixture.context);

            var result = _controller.GetOrders().Result;
            var okResult = result.Result as OkResult;
            var ordersCount = result.Value is not null ? result.Value.Length : 0;

            okResult.Should().NotBeNull();
            ordersCount.Should().Be(3);
        }

        //[Fact]
        public void GetAllOrdersWithOffsetOne_ReturnsArrayOfAllOrdersWithoutFirstOne()
        {
            var _controller = new ClientController(_fixture.context);
            var expected = _fixture.GetSampleOrders().TakeLast(2).ToArray();

            var result = _controller.GetOrders(1).Result;
            var okResult = result.Result as OkResult;

            okResult.Should().NotBeNull();
            result.Value.Should().Equal(expected);
        }

        //[Fact]
        public void GetAllOrdersWithInvalidOffset_ShouldReturnBadRequest()
        {
            var _controller = new ClientController(_fixture.context);

            var result = _controller.GetOrders(-5).Result;
            var badRequestResult = result.Result as BadRequestResult;

            badRequestResult.Should().NotBeNull();
        }

        //[Fact]
        public void GetTwoFirstOrders_ReturnsArrayOfTwoFirstOrders()
        {
            var _controller = new ClientController(_fixture.context);
            var expected = _fixture.GetSampleOrders().Take(2).ToArray();

            var result = _controller.GetOrders(0, 2).Result;
            var okResult = result.Result as OkResult;

            okResult.Should().NotBeNull();
            result.Value.Should().Equal(expected);
        }

        //[Fact]
        public void GetAllOrdersWithInvalidLimit_ShouldReturnBadRequest()
        {
            var _controller = new ClientController(_fixture.context);

            var result = _controller.GetOrders(0, -5).Result;
            var badRequestResult = result.Result as BadRequestResult;

            badRequestResult.Should().NotBeNull();
        }

        //[Fact]
        public void GetOrdersSortedByStartDateDesc_ReturnsArrayOfAllOrdersSortedByStartDateDesc()
        {
            var _controller = new ClientController(_fixture.context);
            var expected = _fixture.GetSampleOrders().OrderByDescending(order => order.StartDate).ToArray();

            var result = _controller.GetOrders(0, 0, "startDate(desc)").Result;
            var okResult = result.Result as OkResult;

            okResult.Should().NotBeNull();
            result.Value.Should().Equal(expected);
        }

        //[Fact]
        public void GetOrdersSortedByInvalidSortString_ShouldReturnBadRequest()
        {
            var _controller = new ClientController(_fixture.context);

            var result = _controller.GetOrders(0, 0, "somesortstring").Result;
            var badRequestResult = result.Result as BadRequestResult;

            badRequestResult.Should().NotBeNull();
        }

        //[Fact]
        public void GetOrdersStartedAtExactDate_ReturnsArrayOfOrdersStartedAtExactDate()
        {
            var _controller = new ClientController(_fixture.context);
            var expected = _fixture.GetSampleOrders().Where(order => order.OrderId == 1).ToArray();

            var result = _controller.GetOrders(0, 0, "", expected.First().StartDate).Result;
            var okResult = result.Result as OkResult;

            okResult.Should().NotBeNull();
            result.Value.Should().Equal(expected);
        }

        //[Fact]
        public void GetOrdersEndedAtExactDate_ReturnsArrayOfOrdersEndedAtExactDate()
        {
            var _controller = new ClientController(_fixture.context);
            var expected = _fixture.GetSampleOrders().Where(order => order.OrderId == 2).ToArray();

            var result = _controller.GetOrders(0, 0, "", null, expected.First().EndDate).Result;
            var okResult = result.Result as OkResult;

            okResult.Should().NotBeNull();
            result.Value.Should().Equal(expected);
        }

        //[Fact]
        public void GetOrdersWithExactPrice_ReturnsArrayOfOrdersWithExactPrice()
        {
            var _controller = new ClientController(_fixture.context);
            var expected = _fixture.GetSampleOrders().Where(order => order.Price == 500).ToArray();

            var result = _controller.GetOrders(0, 0, "", null, null, 500).Result;
            var okResult = result.Result as OkResult;

            okResult.Should().NotBeNull();
            result.Value.Should().Equal(expected);
        }

        //[Fact]
        public void GetOrdersWithInvalidPrice_ShouldReturnBadRequest()
        {
            var _controller = new ClientController(_fixture.context);

            var result = _controller.GetOrders(0, 0, "", null, null, -100).Result;
            var badRequestResult = result.Result as BadRequestResult;

            badRequestResult.Should().NotBeNull();
        }

        //[Fact]
        public void GetOrdersWithPriceBelowGiven_ReturnsArrayOfOrdersWithPriceBelowGiven()
        {
            var _controller = new ClientController(_fixture.context);
            var expected = _fixture.GetSampleOrders().Where(order => order.Price < 600).ToArray();

            var result = _controller.GetOrders(0, 0, "", null, null, 0, 600).Result;
            var okResult = result.Result as OkResult;

            okResult.Should().NotBeNull();
            result.Value.Should().Equal(expected);
        }

        //[Fact]
        public void GetOrdersWithPriceBelowGivenInvalid_ShouldReturnBadRequest()
        {
            var _controller = new ClientController(_fixture.context);

            var result = _controller.GetOrders(0, 0, "", null, null, 0, -100).Result;
            var badRequestResult = result.Result as BadRequestResult;

            badRequestResult.Should().NotBeNull();
        }

        //[Fact]
        public void GetOrdersWithPriceAboveGiven_ReturnsArrayOfOrdersWithPriceAboveGiven()
        {
            var _controller = new ClientController(_fixture.context);
            var expected = _fixture.GetSampleOrders().Where(order => order.Price > 300).ToArray();

            var result = _controller.GetOrders(0, 0, "", null, null, 0, 0, 300).Result;
            var okResult = result.Result as OkResult;

            okResult.Should().NotBeNull();
            result.Value.Should().Equal(expected);
        }

        //[Fact]
        public void GetOrdersWithPriceAboveGivenInvalid_ShouldReturnBadRequest()
        {
            var _controller = new ClientController(_fixture.context);

            var result = _controller.GetOrders(0, 0, "", null, null, 0, 0, -100).Result;
            var badRequestResult = result.Result as BadRequestResult;

            badRequestResult.Should().NotBeNull();
        }

        //[Fact]
        public void GetOrdersWithExactPriceAndAboveGiven_ReturnsEmptyArrayOfOrders()
        {
            var _controller = new ClientController(_fixture.context);
            var expected = _fixture.GetSampleOrders().Where(order => order.Price == 300 && order.Price > 500).ToArray();

            var result = _controller.GetOrders(0, 0, "", null, null, 300, 0, 500).Result;
            var okResult = result.Result as OkResult;

            okResult.Should().NotBeNull();
            result.Value.Should().Equal(expected);
        }

        //[Fact]
        public void GetOrdersWithExactPriceAndBelowGiven_ReturnsEmptyArrayOfOrders()
        {
            var _controller = new ClientController(_fixture.context);
            var expected = _fixture.GetSampleOrders().Where(order => order.Price == 600 && order.Price < 500).ToArray();

            var result = _controller.GetOrders(0, 0, "", null, null, 600, 500).Result;
            var okResult = result.Result as OkResult;

            okResult.Should().NotBeNull();
            result.Value.Should().Equal(expected);
        }

        //[Fact]
        public void PostOrder_ShouldAddOrderToDatabaseAndReturnOk()
        {
            var _controller = new ClientController(_fixture.context);
            var orderDTO = new OrderDTO()
            {
                dietIDs = new string[2] { "1", "2" },
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

        //[Fact]
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

        //[Fact]
        public void PayOrderWithId_ShouldReturnOk()
        {
            var _controller = new ClientController(_fixture.context);

            var okResult = _controller.PayOrder(1).Result as OkResult;

            okResult.Should().NotBeNull();
        }

        //[Fact]
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
            var orders = GetSampleOrders();
            context.Orders.AddRange(orders);
            context.Clients.AddRange(new List<Client>(){
                new Client()
                {
                    ClientId = 1,
                    Name = "John",
                    LastName = "Smith",
                    Email= "john.smith@gmail.com",
                    Address = orders.ElementAt(1).DeliveryDetails.Address,
                    PhoneNumber = "+48123456789"
                },
                new Client()
                {
                    ClientId = 2,
                    Name = "Tom",
                    LastName = "Lukas",
                    Email = "tom.lukas@gmail.com",
                    Address = orders.ElementAt(2).DeliveryDetails.Address,
                    PhoneNumber = "+48987654321"
                }
            });
            context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public IEnumerable<Order> GetSampleOrders()
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
                EndDate = DateTime.Now.AddDays(30),
                Price = 300.0m,
            };
            var order2 = new Order()
            {
                OrderId = 2,
                Diets = new List<Diet>(),
                DeliveryDetails = deliveryDetail2,
                StartDate = DateTime.Now.AddDays(20),
                EndDate = DateTime.Now.AddDays(60),
                Price = 600.0m,
            };
            var order3 = new Order()
            {
                OrderId = 3,
                Diets = new List<Diet>(),
                DeliveryDetails = deliveryDetail1,
                StartDate = DateTime.Now.AddDays(10),
                EndDate = DateTime.Now.AddDays(50),
                Price = 500.0m,
            };
            orders.Add(order1);
            orders.Add(order2);
            orders.Add(order3);

            return orders;
        }
    }
}
