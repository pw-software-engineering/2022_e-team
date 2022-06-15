﻿using ECaterer.Core;
using ECaterer.Core.Models;
using ECaterer.Web.DTO.ClientDTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ECaterer.Web.Infrastructure;
using System.Net.Http;
using System.Net.Http.Json;
using ECaterer.Web.Converters;
using ECaterer.Contracts;
using ECaterer.Contracts.Client;
using System.Linq;
using ECaterer.Web.DTO;
using System.Collections.Generic;
using ECaterer.Contracts.Orders;
using System.Net;

namespace ECaterer.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientController : Controller
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        private readonly ApiClient _apiClient;

        public ClientController(DataContext context, IConfiguration configuration, ApiClient apiClient)
        {
            _context = context;
            _configuration = configuration;
            _apiClient = apiClient;
        }

        [HttpPost("loginall")]
        public async Task<ActionResult<AuthDTO>> Login([FromBody] LoginDTO authData)
        {
            HttpResponseMessage response;

            switch (authData.UserType)
            {
                case UserType.Common:
                    response = await _apiClient.PostAsJsonAsync<LoginUserModel>("/client/login", LoginConverter.Convert(authData));
                    break;
                case UserType.Deliverer:
                    response = await _apiClient.PostAsJsonAsync<LoginUserModel>("/deliverer/login", LoginConverter.Convert(authData));
                    break;
                case UserType.Producer:
                    response = await _apiClient.PostAsJsonAsync<LoginUserModel>("/producer/login", LoginConverter.Convert(authData));
                    break;
                default:
                    return BadRequest();
            }
            

            if (response.IsSuccessStatusCode)
            {
                var token = response.Headers.GetValues("api-key").First();
                return Ok(AuthConverter.ConvertBack(token));
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return Unauthorized();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("registeruser")]
        public async Task<ActionResult<AuthDTO>> Register ([FromBody] RegisterDTO clientData)
        {
            var response = await _apiClient.PostAsJsonAsync<ClientModel>("/Client/Register", RegisterConverter.Convert(clientData));
           
            if (response.IsSuccessStatusCode)
            {
                var token = response.Headers.GetValues("api-key").First();
                return Ok(AuthConverter.ConvertBack(token));
            }
            return BadRequest();
        }

        [HttpGet("getClientOrders")]
        public async Task<ActionResult<ClientOrdersDTO[]>> GetClientOrders()
        {
            var message = new HttpRequestMessage(HttpMethod.Get, "client/orders");
            TokenPropagator.Propagate(Request, message);

            var response = await _apiClient.SendAsync(message);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadFromJsonAsync<OrderClientModel[]>();
                var orders = new ClientOrdersDTO[content.Length];
                for (int i = 0; i < content.Length; i++)
                {
                    orders[i] = OrderConverter.ConvertBack(content[i]);
                }
                return Ok(orders);
            }
            else if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return Unauthorized();
            }
            else
            {
                return BadRequest();
            }

        }

        //[HttpGet("account")]
        //public async Task<ActionResult<ClientDTO>> GetClientData()
        //{
        //    var response = await _apiClient.GetAsync("/Client/Account");

        //    if (response.IsSuccessStatusCode)
        //    {
        //        return Ok()
        //    }
        //    return BadRequest();
        //}

        //[HttpPut("account")]
        //public async Task<ActionResult> EditClientData([FromBody] Client clientData)
        //{
        //    return BadRequest();
        //}

        //[HttpGet("orders")] //wartosci domyslne do ustalenia
        //public async Task<ActionResult<Order[]>> GetOrders(int offset = 0, int limit = 0, string sort = "startDate(asc)", DateTime? startDate = null, DateTime? endDate = null, int price = 0, int price_lt = 0, int price_ht = 0) 
        //{
        //    return BadRequest();
        //}

        //[HttpPost("orders")]
        //public async Task<ActionResult> PostOrder([FromBody] OrderDTO orderDTO)
        //{
        //    return BadRequest();
        //}

        //[HttpPost("orders/{orderId}/complain")]
        //public async Task<ActionResult> PostComplaint(int orderId, [FromBody] ComplaintDTO complaintDTO)
        //{
        //    return BadRequest();
        //}

        //[HttpGet("orders/{orderId}/pay")]
        //public async Task<ActionResult> PayOrder(int orderId)
        //{
        //    return BadRequest();
        //}
    }
}
