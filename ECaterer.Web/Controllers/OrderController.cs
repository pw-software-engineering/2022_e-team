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
        public async Task<ActionResult> SendOrder([FromBody]OrderDTO model)
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

        [HttpGet("getProducerOrders")]
        public async Task<ActionResult<DelivererOrderDTO[]>> GetProducerOrders()
        {
            return Ok(new ProducerOrderDTO[]
            {
                new ProducerOrderDTO()
                {
                    OrderNumber = "1234",
                    OrderDate = DateTime.Now,
                    Status = "Dostarczone"
                },
                new ProducerOrderDTO()
                {
                    OrderNumber = "1234",
                    OrderDate = DateTime.Now,
                    Status = "Dostarczone"
                },
                new ProducerOrderDTO()
                {
                    OrderNumber = "2345",
                    OrderDate = DateTime.Now,
                    Status = "Nie dostarczone"
                }
            });
        }

        [HttpGet("previewOrder/{orderNumber}")]
        public async Task<ActionResult<PreviewOrderDTO>> PreviewOrder(string orderNumber)
        {
            if (orderNumber == "1")
                return Ok(new PreviewOrderDTO()
                {
                    OrderNumber = "1234",
                    DietNames = new List<string>() { "diet1", "diet2" },
                    Comment = "xd",
                    Status = "ToRealized",
                    Address = "Długa 15, Warszawa",
                    Phone = "666-666-666",
                    Cost = 100.05M,
                    OrderDate = DateTime.Now,
                    DeliverDate = DateTime.Now,
                    HasComplaint = true
                });
            else if (orderNumber == "2")
                return Ok(new PreviewOrderDTO()
                {
                    OrderNumber = "1234",
                    DietNames = new List<string>() { "diet1", "diet2" },
                    Comment = "xd",
                    Status = "Paid",
                    Address = "Długa 15, Warszawa",
                    Phone = "666-666-666",
                    Cost = 100.05M,
                    OrderDate = DateTime.Now,
                    DeliverDate = DateTime.Now,
                    HasComplaint = true
                });
            else if(orderNumber == "3")
                return Ok(new PreviewOrderDTO()
                {
                    OrderNumber = "1234",
                    DietNames = new List<string>() { "diet1", "diet2" },
                    Comment = "xd",
                    Status = "ToRealized",
                    Address = "Długa 15, Warszawa",
                    Phone = "666-666-666",
                    Cost = 100.05M,
                    OrderDate = DateTime.Now,
                    DeliverDate = DateTime.Now,
                    HasComplaint = false
                });
            else
                return Ok(new PreviewOrderDTO()
                {
                    OrderNumber = "1234",
                    DietNames = new List<string>() { "diet1", "diet2" },
                    Comment = "xd",
                    Status = "Paid",
                    Address = "Długa 15, Warszawa",
                    Phone = "666-666-666",
                    Cost = 100.05M,
                    OrderDate = DateTime.Now,
                    DeliverDate = DateTime.Now,
                    HasComplaint = false
                });
        }


        [HttpPatch("sendOrderToDeliverer/{orderNumber}")]
        public async Task<ActionResult> SendOrderToDeliverer(string orderNumber)
        {
            return Ok();
        }

        [HttpGet("{orderNumber}/complaint")]
        public async Task<ActionResult<ComplaintDTO>> GetComplaint()
        {
            return Ok(new ComplaintDTO
            {
                Description = "opis reklamacji",
                Status = "Do rozpatrzenia",
                ClientName = "120231",
                ComplaintDate = DateTime.Now
            });
        }
    }
}
