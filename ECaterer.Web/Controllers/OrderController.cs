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
using ECaterer.Contracts.Client;
using ECaterer.Contracts.Producer;

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
                return Ok();
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
                var resultQuery = await response.Content.ReadFromJsonAsync<OrderDelivererModel[]>();
                var resultList = new List<DelivererOrderDTO>();

                foreach (var r in resultQuery)
                {
                    resultList.Add(DelivererOrderConverter.ConvertBack(r));
                }

                return Ok(resultList.ToArray());
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
            var message = new HttpRequestMessage(HttpMethod.Post, "deliverer/orders/" + orderNumber + "/deliver");
            TokenPropagator.Propagate(Request, message);

            var response = await _apiClient.SendAsync(message);

            if (response.IsSuccessStatusCode)
            {
                return Ok();
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

        [HttpGet("getProducerOrders")]
        public async Task<ActionResult<DelivererOrderDTO[]>> GetProducerOrders()
        {
            var message = new HttpRequestMessage(HttpMethod.Get, "/producer/orders");
            TokenPropagator.Propagate(Request, message);

            var response = await _apiClient.SendAsync(message);

            if (response.IsSuccessStatusCode)
            {
                var resultQuery = await response.Content.ReadFromJsonAsync<OrderDelivererModel[]>();
                var resultList = new List<DelivererOrderDTO>();

                foreach (var r in resultQuery)
                {
                    resultList.Add(DelivererOrderConverter.ConvertBack(r));
                }

                return Ok(resultList.ToArray());
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
            var message = new HttpRequestMessage(HttpMethod.Get, "/client/orders");
            TokenPropagator.Propagate(Request, message);

            var response = await _apiClient.SendAsync(message);

            if (response.IsSuccessStatusCode)
            {
                var resultQuery = await response.Content.ReadFromJsonAsync<OrderClientModel[]>();
                var resultList = new List<PreviewOrderDTO>();

                foreach (var r in resultQuery)
                {
                    resultList.Add(ClientOrderConverter.ConvertBack(r));
                }

                var result = resultList.Where(x => x.OrderNumber == orderNumber).FirstOrDefault();
                if (result != null)
                {
                    foreach (var diet in result.DietNames)
                    {
                        var message2 = new HttpRequestMessage(HttpMethod.Get, "/diets/" + diet);
                        TokenPropagator.Propagate(Request, message2);

                        var response2 = await _apiClient.SendAsync(message2);
                        if (response2.IsSuccessStatusCode)
                        {
                            var result2 = await response.Content.ReadFromJsonAsync<DietModel>();

                            foreach (var meal in result2.Meals)
                                result.MealsConcatenated.Append(meal.Name);
                        }
                    }
                }
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
            // meals concatenated should be all meals from diets, concatenated into single string array
           
        }


        [HttpPatch("sendOrderToDeliverer/{orderNumber}")]
        public async Task<ActionResult> SendOrderToDeliverer(string orderNumber)
        {
            return Ok();
        }

        [HttpGet("{orderNumber}/complaint")]
        public async Task<ActionResult<ComplaintOrderDTO>> GetComplaint()
        {
            var message = new HttpRequestMessage(HttpMethod.Get, "/producer/orders/complaints");
            TokenPropagator.Propagate(Request, message);

            var response = await _apiClient.SendAsync(message);

            if (response.IsSuccessStatusCode)
            {
                var resultQuery = await response.Content.ReadFromJsonAsync<ComplaintModel[]>();
                var resultList = new List<ComplaintOrderDTO>();

                foreach (var r in resultQuery)
                {
                    resultList.Add(ComplaintConverter.ConvertBack(r));
                }

                return Ok(resultList.First());
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

        [HttpPost("makeComplaint")]
        public async Task<ActionResult> MakeComplaint([FromBody] MakeComplaintDTO model)
        {
            var message = new HttpRequestMessage(HttpMethod.Post, "client/orders/" + model.OrderNumber + "/complain");
            TokenPropagator.Propagate(Request, message);
            message.Content = JsonContent.Create(new AddComplaintModel { Complain_description = model.Description });
            var response = await _apiClient.SendAsync(message);

            if (response.IsSuccessStatusCode)
            {
                return Ok();
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

        [HttpPatch("cancelComplaint/{orderNumber}")]
        public async Task<ActionResult> CancelComplaint(string orderNumber)
        {
           
                return Ok();
            
        }

        [HttpPost("answerComplaint")]
        public async Task<ActionResult> AnswerComplaint([FromBody] AnswerComplaintDTO model)
        {
            var message = new HttpRequestMessage(HttpMethod.Post, "producer/orders/" + model.OrderNumber + "/answer-complaint");
            TokenPropagator.Propagate(Request, message);
            message.Content = JsonContent.Create(new AnswerComplaintModel { Complaint_answer = model.Answer });
            var response = await _apiClient.SendAsync(message);

            if (response.IsSuccessStatusCode)
            {
                return Ok();
            }
            else if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return Unauthorized();
            }
            else
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}
