using ECaterer.Core;
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

        [HttpPost("loginuser")]
        public async Task<ActionResult<AuthDTO>> Login([FromBody] LoginDTO authData)
        {
            var response = await _apiClient.PostAsJsonAsync<LoginUserModel>("/Client/Login", LoginConverter.Convert(authData));
            var content = await response.Content.ReadFromJsonAsync<AuthenticatedUserModel>();

            if (response.IsSuccessStatusCode)
            {
                return Ok(AuthConverter.ConvertBack(content));
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
                var content = await response.Content.ReadFromJsonAsync<AuthenticatedUserModel>();
                return Ok(AuthConverter.ConvertBack(content));
            }
            return BadRequest();
        }

        [HttpGet("account")]
        public async Task<ActionResult<Client>> GetClientData()
        {
            return BadRequest();
        }

        [HttpPut("account")]
        public async Task<ActionResult> EditClientData([FromBody] Client clientData)
        {
            return BadRequest();
        }

        [HttpGet("orders")] //wartosci domyslne do ustalenia
        public async Task<ActionResult<Order[]>> GetOrders(int offset = 0, int limit = 0, string sort = "startDate(asc)", DateTime? startDate = null, DateTime? endDate = null, int price = 0, int price_lt = 0, int price_ht = 0) 
        {
            return BadRequest();
        }

        [HttpPost("orders")]
        public async Task<ActionResult> PostOrder([FromBody] OrderDTO orderDTO)
        {
            return BadRequest();
        }

        [HttpPost("orders/{orderId}/complain")]
        public async Task<ActionResult> PostComplaint(int orderId, [FromBody] ComplaintDTO complaintDTO)
        {
            return BadRequest();
        }

        [HttpGet("orders/{orderId}/pay")]
        public async Task<ActionResult> PayOrder(int orderId)
        {
            return BadRequest();
        }
    }
}
