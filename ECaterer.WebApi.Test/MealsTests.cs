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

            var response = await Client.PostAsJsonAsync(request.Url, request.Body);

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);

            var auth = response.Headers.GetValues("api-key").FirstOrDefault();

            auth.Should().NotBeNull();

            TokenHandler.SetToken(auth);

        }

        [Fact]
        public async Task BA_TestAddMeal_OK()
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
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task BB_TestAddMeal_Unauthorized()
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, $"/api/meals");
            requestMessage.Content = JsonContent.Create(new MealModel()
            {
                Calories = 450,
                AllergentList = new string[] { "Beaf", "Cheese" },
                IngredientList = new string[] { "Beaf", "Salad", "Bread", "Cheese" },
                Vegan = false
            });

            var response = await Client.SendAsync(requestMessage);
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task BC_TestAddMeal_BadRequest()
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, $"/api/meals");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("api-key", TokenHandler.GetToken());
            requestMessage.Content = JsonContent.Create(new MealModel()
            {
                Calories = 450,
                AllergentList = new string[] { "Beaf", "Cheese" },
                IngredientList = new string[] { "Beaf", "Salad", "Bread", "Cheese" },
                Vegan = false
            });

            var response = await Client.SendAsync(requestMessage);
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task CA_TestGetMeals_OK()
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, $"/api/meals");
            requestMessage.Content = JsonContent.Create(new MealModel()
            {
                Calories = 450,
                AllergentList = new string[] { "Beaf", "Cheese" },
                IngredientList = new string[] { "Beaf", "Salad", "Bread", "Cheese" },
                Vegan = false
            });

            var response = await Client.SendAsync(requestMessage);
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task BC_TestAddMeal_BadRequest()
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, $"/api/meals");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("api-key", TokenHandler.GetToken());
            requestMessage.Content = JsonContent.Create(new MealModel()
            {
                Calories = 450,
                AllergentList = new string[] { "Beaf", "Cheese" },
                IngredientList = new string[] { "Beaf", "Salad", "Bread", "Cheese" },
                Vegan = false
            });

            var response = await Client.SendAsync(requestMessage);
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task CA_TestGetMeals_OK()
        {
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenHandler.GetToken());
            var meals = await Client.GetFromJsonAsync<GetMealsResponseModel[]>("/api/meals");
            meals.Should().NotBeNull();
            meals.Count().Should().NotBe(0);

            var addedMealId = meals.Where(meal => meal.Name == "Pancake" && meal.Calories == 130).Select(meal => meal.Id).FirstOrDefault();

            addedMealId.Should().NotBeNull();

            mealId = addedMealId;
        }

        [Fact]
        public async Task CB_TestGetMeals_Unathorized()
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "/api/meals/");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("api-key", TokenHandler.GetToken());

            var response = await Client.SendAsync(requestMessage);
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task DA_TestEditMeal_OK()
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
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task DB_TestEditMeal_Unauthorized()
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
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task DC_TestEditMeal_BadRequest()
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Put, $"/api/meals/{mealId}");
            requestMessage.Content = JsonContent.Create(new MealModel()
            {
                Calories = 250,
                AllergentList = new string[] { "Milk", "Egg", "Cheese" },
                IngredientList = new string[] { "Milk", "Eggs", "Flour", "Cheese" },
                Vegan = false
            });

            var response = await Client.SendAsync(requestMessage);
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task DD_TestEditMeal_NotFound()
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Put, "/api/meals/unexisting-meal");
            requestMessage.Content = JsonContent.Create(new MealModel()
            {
                Calories = 250,
                AllergentList = new string[] { "Milk", "Egg", "Cheese" },
                IngredientList = new string[] { "Milk", "Eggs", "Flour", "Cheese" },
                Vegan = false
            });

            var response = await Client.SendAsync(requestMessage);
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task EA_TestGetMealById_OK()
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"/api/meals/{mealId}");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("api-key", TokenHandler.GetToken());

            var response = await Client.SendAsync(requestMessage);
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task EB_TestGetMealById_Unauthorized()
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"/api/meals/{mealId}");

            var response = await Client.SendAsync(requestMessage);
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task ED_TestGetMealById_NotFound()
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "/api/meals/unexisting-id");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("api-key", TokenHandler.GetToken());

            var response = await Client.SendAsync(requestMessage);
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task FA_TestDeleteMeal_NotFound()
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Delete, "/api/meals/unexisting-meal");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("api-key", TokenHandler.GetToken());

            var response = await Client.SendAsync(requestMessage);
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task FB_TestDeleteMeal_Unauthorized()
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Delete, $"/api/meals/{mealId}");

            var response = await Client.SendAsync(requestMessage);
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task FC_TestDeleteMeal_BadRequest()
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Delete, $"/api/meals/{mealId}");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("api-key", TokenHandler.GetToken());

            var response = await Client.SendAsync(requestMessage);
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task FD_TestDeleteMeal_OK()
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Delete, $"/api/meals/{mealId}");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("api-key", TokenHandler.GetToken());

            var response = await Client.SendAsync(requestMessage);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
