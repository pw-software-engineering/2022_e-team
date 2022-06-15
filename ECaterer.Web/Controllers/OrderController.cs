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
using System.Linq;
using ECaterer.Contracts.Deliverer;
using System.Net.Http;

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
        public async Task<ActionResult> SendOrder([FromBody]ClientOrderDTO model)
        {
            var message = new HttpRequestMessage(HttpMethod.Post, "client/orders");
            TokenPropagator.Propagate(Request, message);
            message.Content = JsonContent.Create(OrderConverter.Convert(model));
            
            var response = await _apiClient.SendAsync(message);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<DelivererOrderDTO[]>();
                return Ok(result);
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

        [HttpGet("getDelivererOrders")]
        public async Task<ActionResult<DelivererOrderDTO[]>> GetDelivererOrders()
        {
            var message = new HttpRequestMessage(HttpMethod.Get, "deliverer/orders");
            TokenPropagator.Propagate(Request, message);

            var response = await _apiClient.SendAsync(message);

            if(response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<DelivererOrderDTO[]>();
                return Ok(result);
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

        [HttpGet("getDelivererHistory")]
        public async Task<ActionResult<IEnumerable<DelivererHistoryDTO>>> GetDelivererHistory()
        {
            var message = new HttpRequestMessage(HttpMethod.Get, "/deliverer/history");
            TokenPropagator.Propagate(Request, message);

            var response = await _apiClient.SendAsync(message);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<DelivererHistoryDTO>();
                return Ok(result);
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

        [HttpPatch("deliverOrder/{orderNumber}")]
        public async Task<ActionResult> DeliverOrder(string orderNumber)
        {
            return Ok();
        }

        [HttpGet("getProducerOrders")]
        public async Task<ActionResult<DelivererOrderDTO[]>> GetProducerOrders()
        {
            var message = new HttpRequestMessage(HttpMethod.Get, "/producer/orders");
            TokenPropagator.Propagate(Request, message);

            var response = await _apiClient.SendAsync(message);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<DelivererOrderDTO[]>();
                return Ok(result);
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

        [HttpGet("previewOrder/{orderNumber}")]
        public async Task<ActionResult<PreviewOrderDTO>> PreviewOrder(string orderNumber)
        {
            // meals concatenated should be all meals from diets, concatenated into single string array
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
                    HasComplaint = true,
                    MealsConcatenated = new List<string> { "meatballs", "meatballs2" }
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
                    HasComplaint = true,
                    MealsConcatenated = new List<string> { "meatballs", "meatballs2" }
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
                    HasComplaint = false,
                    MealsConcatenated = new List<string> { "meatballs", "meatballs2" }
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
                    HasComplaint = false,
                    MealsConcatenated = new List<string> { "meatballs", "meatballs2" }
                });
        }


        [HttpPatch("sendOrderToDeliverer/{orderNumber}")]
        public async Task<ActionResult> SendOrderToDeliverer(string orderNumber)
        {
            return Ok();
        }

        [HttpGet("{orderNumber}/complaint")]
        public async Task<ActionResult<ComplaintOrderDTO>> GetComplaint()
        {
            return Ok(new ComplaintOrderDTO
            {
                Description = "opis reklamacji",
                Status = "Do rozpatrzenia",
                ClientName = "120231",
                ComplaintDate = DateTime.Now
            });
        }

        [HttpPost("makeComplaint")]
        public async Task<ActionResult> MakeComplaint([FromBody] MakeComplaintDTO model)
        {
            return Ok();
        }

        [HttpPatch("cancelComplaint/{orderNumber}")]
        public async Task<ActionResult> CancelComplaint(string orderNumber)
        {
            return Ok();
        }

        [HttpPost("answerComplaint")]
        public async Task<ActionResult> AnswerComplaint([FromBody] AnswerComplaintDTO model)
        {
            return Ok();
        }
    }
}
