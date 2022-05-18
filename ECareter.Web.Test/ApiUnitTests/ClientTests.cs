using System;
using System.Collections.Generic;
using ECaterer.Core;
using FluentAssertions;
using Xunit;
using Microsoft.EntityFrameworkCore;
using ECaterer.Core.Models;
using System.Linq;
using Microsoft.AspNetCore.Http;
using ECaterer.Web.DTO.ClientDTO;
using ECaterer.WebApi.Controllers;
using ECaterer.WebApi.Services;
using Moq;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ECaterer.Contracts;
using ECaterer.Contracts.Client;
using ECaterer.WebApi.Common.Interfaces;

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
        public async void LoginClient_ShouldReturnTokenAndOk()
        {
            var mockUserManager = _fixture.GetMockUserManager();
            var mockSignInManager = _fixture.GetMockSignInManager(mockUserManager);
            var ordersService = _fixture.GetMockOrderService();
            var johnSmith = _fixture.context.Clients.FirstOrDefault();

            mockUserManager
               .Setup(r => r.FindByEmailAsync(
                   It.Is<string>(email => email == johnSmith.Email)))
               .ReturnsAsync(new IdentityUser()
               {
                   Email = johnSmith.Email,
                   UserName = johnSmith.Email
               });
            mockSignInManager
                .Setup(r => r.CheckPasswordSignInAsync(
                    It.Is<IdentityUser>(user => user.Email == johnSmith.Email),
                    It.Is<string>(password => password == "12345678"),
                    false))
                .ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Success);
            var _controller = new ClientController(mockUserManager.Object, mockSignInManager.Object, new TokenService(new ApplicationDbContext(new DbContextOptions<ApplicationDbContext>())), _fixture.context, ordersService.Object);
            _controller.ControllerContext.HttpContext = new DefaultHttpContext();

            LoginUserModel loginUserModel = new LoginUserModel() 
            { 
                Email = johnSmith.Email,
                Password = "12345678"
            };

            var result = await _controller.Login(loginUserModel);
            var okResult = result as OkResult;
            var token = _controller.Response.Headers["api-key"];

            okResult.Should().NotBeNull();
            token.Should().NotBeNull();
        }

        [Fact]
        public async void RegisterClient_ShouldReturnTokenAndOk()
        {
            var mockUserManager = _fixture.GetMockUserManager();
            var mockSignInManager = _fixture.GetMockSignInManager(mockUserManager);
            var ordersService = _fixture.GetMockOrderService();

            var adambrown = new ClientModel()
            {
                Name = "Adam",
                LastName = "Brown",
                Email = "adam.brown@gmail.com",
                Address = new AddressModel() 
                {
                    Street = "Street 3",
                    BuildingNumber = "3",
                    ApartmentNumber = "3",
                    PostCode = "00-530",
                    City = "Krakow3"
                },
                PhoneNumber = "+48135792468",
                Password = "12345678"
            };

            mockUserManager
                .Setup(r => r.CreateAsync(
                    It.Is<IdentityUser>(user => user.Email == adambrown.Email),
                    It.Is<string>(password => password == "12345678")))
                .ReturnsAsync(IdentityResult.Success);
            var _controller = new ClientController(mockUserManager.Object, mockSignInManager.Object, new TokenService(new ApplicationDbContext(new DbContextOptions<ApplicationDbContext>())), _fixture.context, ordersService.Object);
            _controller.ControllerContext.HttpContext = new DefaultHttpContext();

            var result = await _controller.Register(new ClientModel() 
            { 
                Address = adambrown.Address,
                Email = adambrown.Email,
                LastName = adambrown.LastName,
                Name = adambrown.Name,
                Password = adambrown.Password,
                PhoneNumber = adambrown.PhoneNumber
            });
            var okResult = result as OkObjectResult;

            okResult.Should().NotBeNull();

            var tokenFromHeader = _controller.Response.Headers["api-key"];
            var tokenFromModel = okResult.Value as AuthenticatedUserModel;

            tokenFromHeader.Should().NotBeNull();
            tokenFromModel.Should().NotBeNull();
            tokenFromHeader.Should().Equal(tokenFromModel.Token);

            var lastAddedUser = _fixture.context.Clients.Last();
            var lastAddedUserEmail = lastAddedUser.Email;
            if (lastAddedUserEmail.Equals(adambrown.Email))
            {
                _fixture.context.Clients.Remove(lastAddedUser);
                _fixture.context.SaveChanges();
            }

            lastAddedUserEmail.Should().Be(adambrown.Email);
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
                    ClientId = "client_1",
                    Name = "John",
                    LastName = "Smith",
                    Email= "john.smith@gmail.com",
                    Address = orders.ElementAt(1).DeliveryDetails.Address,
                    PhoneNumber = "+48123456789"
                },
                new Client()
                {
                    ClientId =  "client_2",
                    Name = "Tom",
                    LastName = "Lukas",
                    Email = "tom.lukas@gmail.com",
                    Address = orders.ElementAt(2).DeliveryDetails.Address,
                    PhoneNumber = "+48987654321"
                }
            });
            context.SaveChanges();
        }

        public IEnumerable<Order> GetSampleOrders()
        {
            var orders = new List<Order>();
            var address1 = new Address()
            {
                AddressId = "address_1",
                Street = "Street 1",
                BuildingNumber = "1",
                ApartmentNumber = "1",
                PostCode = "00-250",
                City = "Warsaw"
            };
            var address2 = new Address()
            {
                AddressId = "address_2",
                Street = "Street 2",
                BuildingNumber = "2",
                ApartmentNumber = "2",
                PostCode = "00-520",
                City = "Krakow"
            };
            var deliveryDetail1 = new DeliveryDetails()
            {
                DeliveryDetailsId = "details_1",
                Address = address1,
                PhoneNumber = "+48123456789"
            };
            var deliveryDetail2 = new DeliveryDetails()
            {
                DeliveryDetailsId = "details_2",
                Address = address2,
                PhoneNumber = "+48987654321"
            };
            var order1 = new Order()
            {
                OrderId = "order_1",
                Diets = new List<Diet>(),
                DeliveryDetails = deliveryDetail1,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(30),
                Price = 300.0m,
            };
            var order2 = new Order()
            {
                OrderId = "order_2",
                Diets = new List<Diet>(),
                DeliveryDetails = deliveryDetail2,
                StartDate = DateTime.Now.AddDays(20),
                EndDate = DateTime.Now.AddDays(60),
                Price = 600.0m,
            };
            var order3 = new Order()
            {
                OrderId = "order_3",
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

        public Mock<UserManager<IdentityUser>> GetMockUserManager()
        {
            return new Mock<UserManager<IdentityUser>>(
                new Mock<IUserStore<IdentityUser>>().Object,
                new Mock<IOptions<IdentityOptions>>().Object,
                new Mock<IPasswordHasher<IdentityUser>>().Object,
                new IUserValidator<IdentityUser>[0],
                new IPasswordValidator<IdentityUser>[0],
                new Mock<ILookupNormalizer>().Object,
                new Mock<IdentityErrorDescriber>().Object,
                new Mock<IServiceProvider>().Object,
                new Mock<ILogger<UserManager<IdentityUser>>>().Object);
        }

        public Mock<SignInManager<IdentityUser>> GetMockSignInManager(Mock<UserManager<IdentityUser>> mockUserManager)
        {
            return new Mock<SignInManager<IdentityUser>>(
                mockUserManager.Object,
                new Mock<IHttpContextAccessor>().Object,
                 new Mock<IUserClaimsPrincipalFactory<IdentityUser>>().Object,
                 new Mock<IOptions<IdentityOptions>>().Object,
                 new Mock<ILogger<SignInManager<IdentityUser>>>().Object,
                 new Mock<IAuthenticationSchemeProvider>().Object,
                 new Mock<IUserConfirmation<IdentityUser>>().Object);
        }

        public Mock<IOrdersService> GetMockOrderService()
        {
            return new Mock<IOrdersService>();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
