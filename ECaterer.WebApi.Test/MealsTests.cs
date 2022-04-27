using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ECaterer.Contracts;
using FluentAssertions;
using Xunit;
using ECaterer.WebApi.Data;
using System.Net.Http.Json;
using ECaterer.Contracts.Client;
using System.Linq;
using System.Net.Http.Headers;
using System.Collections.Generic;
using ECaterer.Contracts.Meals;
using System.Net;

namespace ECaterer.WebApi.Integration.Test
{
    [TestCaseOrderer("ECaterer.WebApi.Integration.Test.AlphabeticalOrderer", "ECaterer.WebApi.Integration.Test")]
    public class MealsTests : IClassFixture<TestFixture<Startup>>
    {
        private HttpClient Client;
        private static string mealId;

        public MealsTests(TestFixture<Startup> fixture)
        {
            Client = fixture.Client;
        }

        [Fact]
        public async Task AATestLoginProducer()
        {
            var request = new
            {
                Url = "/api/client/login",
                Body = CredentialResolver.ResolveProducer()
            };

            var response = await Client.PostAsJsonAsync<LoginUserModel>(request.Url, request.Body);

            response.EnsureSuccessStatusCode();

            var auth = response.Headers.GetValues("api-key").FirstOrDefault();

            auth.Should().NotBeNull();

            TokenHandler.SetToken(auth);

        }

        [Fact]
        public async Task BATestAddMeal()
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, $"/api/meals");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("api-key", TokenHandler.GetToken());
            requestMessage.Content = JsonContent.Create(new MealModel()
            {
                Name = "Pancake",
                Calories = 130,
                AllergentList = new string[] { "Milk", "Egg" },
                IngredientList = new string[] { "Milk", "Egg", "Flour" },
                Vegan = false
            });

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
