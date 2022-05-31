using ECaterer.Contracts;
using ECaterer.Contracts.Client;
using ECaterer.Contracts.Deliverer;
using ECaterer.Contracts.Meals;
using ECaterer.Contracts.Orders;
using ECaterer.Core.Models;
using FluentAssertions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ECaterer.WebApi.Integration.Test
{
    [TestCaseOrderer("ECaterer.WebApi.Integration.Test.AlphabeticalOrderer", "ECaterer.WebApi.Integration.Test")]
    public class DelivererTests : IClassFixture<TestFixture<Startup>>
    {
        private HttpClient Client;
        private static string mealId;

        public DelivererTests(TestFixture<Startup> fixture)
        {
            Client = fixture.Client;
        }
         
        // Returns api-key
        private async Task LogAndSeedDatabase()
        {
            // Add token to headers
            var loginRequest = new
            {
                Url = "/deliverer/login",
                Body = CredentialResolver.ResolveDeliverer()
            };

            var loginResponse = await Client.PostAsJsonAsync<LoginUserModel>(loginRequest.Url, loginRequest.Body);
            var auth = loginResponse.Headers.GetValues("api-key").FirstOrDefault();
            TokenHandler.SetToken(auth);

            //Seed database
            var addOrderMessage = new HttpRequestMessage(HttpMethod.Post, $"/client/orders");
            addOrderMessage.Headers.Authorization = new AuthenticationHeaderValue("api-key", TokenHandler.GetToken());
            addOrderMessage.Content = JsonContent.Create(new CreateOrderModel
            {
                DeliveryDetails = new DeliveryDetailsModel
                {
                    Address = new AddressModel
                    {
                        Street = "Testowa",
                        BuildingNumber = "12345",
                        ApartmentNumber = "12345",
                        PostCode = "12-345",
                        City = "Testowe"
                    },
                    PhoneNumber = "010101999"
                },
                DietIDs = new List<string>(),
                StartDate = DateTime.Now.AddDays(1),
                EndDate = DateTime.Now.AddDays(10),
            });

            var addResponse = await Client.SendAsync(addOrderMessage);
        }

        [Fact]
        public async Task AATestLoginDeliverer()
        {
            var request = new
            {
                Url = "/deliverer/login",
                Body = CredentialResolver.ResolveDeliverer()
            };

            var response = await Client.PostAsJsonAsync<LoginUserModel>(request.Url, request.Body);

            response.EnsureSuccessStatusCode();

            var auth = response.Headers.GetValues("api-key").FirstOrDefault();

            auth.Should().NotBeNull();

            TokenHandler.SetToken(auth);
        }



        [Fact]
        public async Task ABTestGetDietsFromSeed()
        {
            await LogAndSeedDatabase();

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"/deliverer/orders");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("api-key", TokenHandler.GetToken());

            var response = await Client.SendAsync(requestMessage);
            response.EnsureSuccessStatusCode();

            string jsonContent = response.Content.ReadAsStringAsync().Result;
            ICollection<OrderDelivererModel> orders = JsonConvert.DeserializeObject<ICollection<OrderDelivererModel>>(jsonContent);

            var order = orders.Where(o => o.DeliveryDetails.PhoneNumber == "010101999").FirstOrDefault();

            order.Should().NotBeNull();
        }
        [Fact]
        public async Task ACTestGetDietsFromSeedUnauthorized()
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"/deliverer/orders");

            var response = await Client.SendAsync(requestMessage);
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }
        [Fact]
        public async Task ADTestFulfillDelivery()
        {
            // Get delivery ID
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"/deliverer/orders");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("api-key", TokenHandler.GetToken());

            var response = await Client.SendAsync(requestMessage);
            string jsonContent = response.Content.ReadAsStringAsync().Result;
            ICollection<OrderDelivererModel> orders = JsonConvert.DeserializeObject<ICollection<OrderDelivererModel>>(jsonContent);

            var order = orders.Where(o => o.DeliveryDetails.PhoneNumber == "010101999").FirstOrDefault();
            var id = order.Id;

            // Fulfill
            var fulfillMessage = new HttpRequestMessage(HttpMethod.Post, $"/deliverer/orders/{id}/deliver");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("api-key", TokenHandler.GetToken());

            var fulfillResponse = await Client.SendAsync(requestMessage);
            fulfillResponse.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task AETestGetDietsFromSeedUnauthorized()
        {
            // Get delivery ID
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"/deliverer/orders");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("api-key", TokenHandler.GetToken());

            var response = await Client.SendAsync(requestMessage);
            string jsonContent = response.Content.ReadAsStringAsync().Result;
            ICollection<OrderDelivererModel> orders = JsonConvert.DeserializeObject<ICollection<OrderDelivererModel>>(jsonContent);

            var order = orders.Where(o => o.DeliveryDetails.PhoneNumber == "010101999").FirstOrDefault();
            var id = order.Id;

            // Fulfill
            var fulfillMessage = new HttpRequestMessage(HttpMethod.Post, $"/deliverer/orders/{id}/deliver");

            var fulfillResponse = await Client.SendAsync(requestMessage);
            fulfillResponse.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }
    }
}
