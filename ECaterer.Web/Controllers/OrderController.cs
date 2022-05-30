using ECaterer.Core.Models;
using ECaterer.Web.DTO;
using ECaterer.Web.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Text.Json;
using ECaterer.Contracts.Orders;
using ECaterer.Web.Converters;
using System.Web;
using System.Collections.Generic;

namespace ECaterer.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : Controller
    {
        private readonly ApiClient _apiClient;

        public OrderController(ApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        [HttpPost("sendOrder")]
<<<<<<< Updated upstream
        public async Task<ActionResult> SendOrder([FromBody]ClientOrderDTO model)
=======
        public async Task<ActionResult> SendOrder([FromBody]DTO.ClientDTO.OrderDTO model)
>>>>>>> Stashed changes
        {
            //if (model.address == null)
            //{
            //    /* fetch address for current user */
            //}
            return Ok();
        }

        [HttpGet("getDelivererOrders")]
        public async Task<ActionResult<DelivererOrderDTO[]>> GetDelivererOrders()
        {
            // we need a way to convert address to string, probably...
            return Ok(new DelivererOrderDTO[]
            {
                new DelivererOrderDTO()
                {
                    OrderNumber = "1234",
                    Address = "Długa 15, Warszawa",
                    Phone = "666-666-666",
                    Comment = ""
                },
                new DelivererOrderDTO()
                {
                    OrderNumber = "1234",
                    Address = "Długa 15, Warszawa",
                    Phone = "666-666-666",
                    Comment = "nie dzwonić"
                },
                new DelivererOrderDTO()
                {
                    OrderNumber = "1234",
                    Address = "Długa 15, Warszawa",
                    Phone = "666-666-666",
                    Comment = ""
                }
            });
        }

        [HttpPatch("deliverOrder/{orderNumber}")]
        public async Task<ActionResult> DeliverOrder(string orderNumber)
        {
            return Ok();
        }
    }
}
