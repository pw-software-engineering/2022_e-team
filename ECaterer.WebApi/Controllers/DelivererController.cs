using AutoMapper;
using ECaterer.Contracts;
using ECaterer.Contracts.Client;
using ECaterer.Contracts.Deliverer;
using ECaterer.Contracts.Orders;
using ECaterer.Core;
using ECaterer.Core.Models;
using ECaterer.Core.Models.Enums;
using ECaterer.WebApi.Common.Interfaces;
using ECaterer.WebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace ECaterer.WebApi.Controllers
{
    [Route("deliverer")]
    [ApiController]
    public class DelivererController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly ClientController _clientController;
        private readonly IOrderService _ordersService;
        private readonly Mapper _mapper;

        public DelivererController(DataContext context, IOrderService ordersService, ClientController clientController)
        {
            _context = context;
            _clientController = clientController;
            _ordersService = ordersService;

            var mapperConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<Address, AddressModel>();
                config.CreateMap<DeliveryDetails, DeliveryDetailsModel>();
                config.CreateMap<Order, OrderDelivererModel>()
                .ForMember(
                    dest => dest.Id,
                    act => act.MapFrom(src => src.OrderId));
            });

            _mapper = new Mapper(mapperConfig);
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginUserModel loginUser)
        {
            _clientController.ControllerContext = new ControllerContext(ControllerContext);
            var response = await _clientController.Login(loginUser);
            ControllerContext = new ControllerContext(_clientController.ControllerContext);

            return response;
        }

        [HttpGet("orders")]
        [Authorize/*(Roles = "deliverer")*/]
        public async Task<ActionResult<OrderDelivererModel[]>> GetOrdersToDeliver()
        {
            try
            {
                var ordersWithStatusPrepared = await _ordersService.GetOrders(new GetOrdersDelivererQueryModel());
                var deliveryDetailsDTO = ordersWithStatusPrepared.Select(o => _mapper.Map<OrderDelivererModel>(o)).ToArray();
                return Ok(deliveryDetailsDTO);
            }
            catch
            {
                return BadRequest("Niepowodzenie pobierania");
            }
        }

        [HttpPost("orders/{orderID}/deliver")]
        [Authorize/*(Roles = "deliverer")*/]
        public async Task<ActionResult> FinishDelivery([FromQuery] string orderID)
        {
            try
            {
                var order = _context.Orders.FirstOrDefault(order => order.OrderId == orderID);
                if (order is null)
                    return NotFound("Podane zamówienie nie istnieje");
                if (order.Status != (int)OrderStatus.Prepared)
                    return BadRequest("Niepowodzenie potwierdzenia dostawy");
                order.Status = (int)OrderStatus.Delivered;
                _context.Orders.Update(order);
                await _context.SaveChangesAsync();
                return Ok("Powodzenie potwierdzenia dostawy");
            }
            catch
            {
                return BadRequest("Niepowodzenie potwierdzenia dostawy");
            }
        }
    }
}
