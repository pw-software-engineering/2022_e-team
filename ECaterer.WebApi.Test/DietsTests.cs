using ECaterer.Contracts.Diets;
using ECaterer.Contracts.Meals;
using ECaterer.Contracts.Orders;
using FluentAssertions;
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
    public class DietsTests : IClassFixture<TestFixture<Startup>>
    {
        private HttpClient Client;
        private static string dietId;
        private static string authToken = "as";

        public DietsTests(TestFixture<Startup> fixture)
        {
            Client = fixture.Client;
        }

        [Fact]
        public async Task BATestAddDiet()
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, $"/api/diets");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("api-key", authToken);
            requestMessage.Content = JsonContent.Create(new DietModel()
            {
                Name = "Sport",
                Description = "Diet to get fit fast",
                Meals = new MealModel()
                {
                    Name = "Soup",
                    Calories = 250,
                    AllergentList = new string[] { "Onion", "Cheese" },
                    IngredientList = new string[] { "Water", "Pork", "Onion", "Cheese" },
                    Vegan = false
                },
                Price = 55,
                Vegan = false
            });

            var response = await Client.SendAsync(requestMessage);
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task BBTestGetDiets()
        {
            // get based on controller and contracts
            var diets = await Client.GetFromJsonAsync<GetDietModel[]>("/api/diets");
            diets.Should().NotBeNull();
            diets.Count().Should().NotBe(0);

            var addedDietId = diets.Where(diet => diet.Name == "Sport" && diet.Price == 55).Select(diet => diet.Id).FirstOrDefault();

            addedDietId.Should().NotBeNull();

            dietId = addedDietId;
        }

        [Fact]
        public async Task BCTestEditDietChangePrice()
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Put, $"/api/diets/{dietId}");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("api-key", authToken);
            requestMessage.Content = JsonContent.Create(new DietModel()
            {
                Name = "Sport",
                Description = "Diet to get fit fast",
                Meals = new MealModel()
                {
                    Name = "Soup",
                    Calories = 250,
                    AllergentList = new string[] { "Onion", "Cheese" },
                    IngredientList = new string[] { "Water", "Pork", "Onion", "Cheese" },
                    Vegan = false
                },
                Price = 60,
                Vegan = false
            });

            var response = await Client.SendAsync(requestMessage);
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task BDTestGetDietById()
        {
            var diet = await Client.GetFromJsonAsync<DietModel>($"/api/diets/{dietId}");

            diet.Name.Should().Be("Sport");
        }

        [Fact]
        public async Task BETestDeleteDiet()
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Delete, $"/api/diets/{dietId}");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("api-key", authToken);

            var responseMessage = await Client.SendAsync(requestMessage);

            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
