using ECaterer.Core;
using ECaterer.Core.Models;
using ECaterer.Web.DTO.ClientDTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECaterer.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientController : Controller
    {
        private readonly ILogger<ClientController> _logger;
        private readonly DataContext _context;

        public ClientController(ILogger<ClientController> logger, DataContext context)
        {
            _logger = logger;
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
        public async Task<IActionResult> EditClientData([FromBody] Client clientData)
        {
            return BadRequest();
        }

        [HttpGet("/orders")]
        public async Task<ActionResult<Order[]>> GetOrders(int offset, int limit, string sort, DateTime startDate, DateTime endDate, int price, int price_lt, int price_ht)
        {
            return BadRequest();
        }

        [HttpPost("/orders")]
        public async Task<IActionResult> PostOrder([FromBody] OrderDTO orderDTO)
        {
            return BadRequest();
        }

        [HttpPost("/orders/{orderId}/complain")]
        public async Task<IActionResult> PostComplain(int orderId, [FromBody] ComplainDTO complainDTO)
        {
            return BadRequest();
        }

        [HttpGet("/orders/{orderId}/pay")]
        public async Task<IActionResult> PayOrder(int orderId)
        {
            return BadRequest();
        }
    }
}
