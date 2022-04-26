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

namespace ECaterer.WebApi.Integration.Test
{
    [TestCaseOrderer("ECaterer.WebApi.Integration.Test.AlphabeticalOrderer", "ECaterer.WebApi.Integration.Test")]
    public class ClientTests : IClassFixture<TestFixture<Startup>>
    {
        private HttpClient Client;

        private static string random = new Random().Next().ToString();

        private static string authToken;

        public ClientTests(TestFixture<Startup> fixture)
        {
            Client = fixture.Client;
        }

        [Fact]
        public async Task AATestRegisterUser()
        {
            var request = new
            {
                Url = "/api/client/register",
                Body = new ClientModel()
                {
                    Email = $"{random}@gmail.com",
                    Address = new Contracts.Client.AddressModel()
                    {
                        City = "Strzelno",
                        BuildingNumber = "1",
                        Street = "Kościuszki",
                        PostCode = "88-320"
                    },
                    PhoneNumber = "+48-666-666-666",
                    Name = "Jan",
                    LastName = "Kowalski",
                    Password = "1234!Aaaa"
                }   
            };

            var response = await Client.PostAsJsonAsync<ClientModel>(request.Url, request.Body);
            response.EnsureSuccessStatusCode();


            var value = await response.Content.ReadFromJsonAsync<AuthenticatedUserModel>();

            value.Token.Should().NotBeEmpty();

            authToken = value.Token;

        }

        [Fact]
        public async Task ABTestLoginUser()
        {
            var request = new
            {
                Url = "/api/client/login",
                Body = new LoginUserModel()
                {
                    Email = $"{random}@gmail.com",
                    Password = "1234!Aaaa"
                }
            };

            var response = await Client.PostAsJsonAsync<LoginUserModel>(request.Url, request.Body);

            response.EnsureSuccessStatusCode();

            var auth = response.Headers.GetValues("api-key").FirstOrDefault();

            auth.Should().NotBeNull();

            authToken = auth;

        }

        [Fact]
        public async Task ACTestGetAccountUser()
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "/api/client/account");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("api-key", authToken);

            var response = await Client.SendAsync(requestMessage);

            response.EnsureSuccessStatusCode();

            var clientContent = await response.Content.ReadFromJsonAsync<ClientModel>();

            clientContent.Email.Should().Be($"{random}@gmail.com");

        }

        [Fact]
        public async Task ADTestPutAccountUser()
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Put, "/api/client/account");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("api-key", authToken);
            requestMessage.Content = JsonContent.Create(new ClientModel()
            {
                Email = $"{random}@gmail.com",
                Address = new Contracts.Client.AddressModel()
                {
                    City = "Warszawa",
                    BuildingNumber = "1",
                    Street = "Kościuszki",
                    PostCode = "88-320"
                },
                PhoneNumber = "+48-666-666-666",
                Name = "Jan",
                LastName = "Kowalski",
                Password = "1234!Aaaa"
            });

            var response = await Client.SendAsync(requestMessage);

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task AETestGetOrders()
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "/api/client/orders");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("api-key", authToken);
            
            var parameters = new Dictionary<string, string>() { { "limit", "10" } };
            

            requestMessage.Content = new FormUrlEncodedContent(parameters);
            var response = await Client.SendAsync(requestMessage);

            response.EnsureSuccessStatusCode();
        }
    }
}