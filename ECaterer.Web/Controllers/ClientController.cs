using ECaterer.Core;
using ECaterer.Core.Models;
using ECaterer.Web.DTO.ClientDTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace ECaterer.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientController : Controller
    {
        private readonly DataContext _context;

        public ClientController(DataContext context)
        {
            _context = context;
        }

        [HttpPost("/login")]
        public async Task<ActionResult<ApiKeyAuthTMP>> Login([FromBody] AuthDataTMP authData)
        {
            return BadRequest();
        }

        [HttpPost("/register")]
        public async Task<ActionResult<ApiKeyAuthTMP>> Register([FromBody] Client clientData)
        {
            return BadRequest();
        }

        [HttpGet("/client/account")]
        public async Task<ActionResult<Client>> GetClientData()
        {
            return BadRequest();
        }

        [HttpPut("/client/account")]
        public async Task<ActionResult> EditClientData([FromBody] Client clientData)
        {
            return BadRequest();
        }

        [HttpGet("/orders")] //wartosci domyslne do ustalenia
        public async Task<ActionResult<Order[]>> GetOrders(int offset = 0, int limit = 0, string sort = "startDate(asc)", DateTime? startDate = null, DateTime? endDate = null, int price = 0, int price_lt = 0, int price_ht = 0) 
        {
            return BadRequest();
        }

        [HttpPost("/orders")]
        public async Task<ActionResult> PostOrder([FromBody] OrderDTO orderDTO)
        {
            return BadRequest();
        }

        [HttpPost("/orders/{orderId}/complain")]
        public async Task<ActionResult> PostComplaint(int orderId, [FromBody] ComplaintDTO complaintDTO)
        {
            return BadRequest();
        }

        [HttpGet("/orders/{orderId}/pay")]
        public async Task<ActionResult> PayOrder(int orderId)
        {
            return BadRequest();
        }
    }
}
