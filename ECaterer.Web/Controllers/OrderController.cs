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
    }
}
