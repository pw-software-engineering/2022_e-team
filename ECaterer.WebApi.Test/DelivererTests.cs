using ECaterer.Contracts;
using ECaterer.Contracts.Client;
using ECaterer.Contracts.Meals;
using ECaterer.Contracts.Orders;
using ECaterer.Core.Models;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
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
                    PhoneNumber = "123456789"
                },
                DietIDs = new List<string>(),
                StartDate = DateTime.Now.AddDays(1),
                EndDate = DateTime.Now.AddDays(10),
            });

            var addResponse = await Client.SendAsync(addOrderMessage);
        }

        // TODO
        //  private async Task CleanDatabase()

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
        }

        [Fact]
        public async Task BBTestGetMeals()
        {
            var meals = await Client.GetFromJsonAsync<GetMealsResponseModel[]>("/api/meals");
            meals.Should().NotBeNull();
            meals.Count().Should().NotBe(0);

            var addedMealId = meals.Where(meal => meal.Name == "Pancake" && meal.Calories == 130).Select(meal => meal.Id).FirstOrDefault();

            addedMealId.Should().NotBeNull();

            mealId = addedMealId;
        }

        [Fact]
        public async Task BCTestEditMeal()
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Put, $"/api/meals/{mealId}");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("api-key", TokenHandler.GetToken());
            requestMessage.Content = JsonContent.Create(new MealModel()
            {
                Name = "Cheesecake",
                Calories = 250,
                AllergentList = new string[] { "Milk", "Egg", "Cheese" },
                IngredientList = new string[] { "Milk", "Eggs", "Flour", "Cheese" },
                Vegan = false
            });

            var response = await Client.SendAsync(requestMessage);
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task BDTestGetMealById()
        {
            var meal = await Client.GetFromJsonAsync<MealModel>($"/api/meals/{mealId}");

            meal.Name.Should().Be("Cheesecake");
        }

        [Fact]
        public async Task BETestDeleteMeal()
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Delete, $"/api/meals/{mealId}");

            var responseMessage = await Client.SendAsync(requestMessage);

            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
