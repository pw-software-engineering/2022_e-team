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
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly TokenService _tokenService;
        private readonly DataContext _context;
        private readonly IOrdersService _ordersService;
        private readonly Mapper _mapper;

        public DelivererController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, TokenService tokenService, DataContext context, IOrdersService ordersService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _context = context;
            _ordersService = ordersService;

            var mapperConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<Address, AddressModel>();
                config.CreateMap<DeliveryDetails, DeliveryDetailsModel>()
                .ForMember(
                    dest => dest.Address,
                    act => act.MapFrom(src => src.Address));
                config.CreateMap<Order, DeliveryItemModel>()
                .ForMember(
                    dest => dest.Id,
                    act => act.MapFrom(src => src.OrderId))
                .ForMember(
                    dest => dest.DeliveryDetails,
                    act => act.MapFrom(src => src.DeliveryDetails));
            });

            _mapper = new Mapper(mapperConfig);
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginUserModel loginUser)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(loginUser.Email);

                if (user == null)
                    return BadRequest("Niepowodzenie logowania");

                var result = await _signInManager.CheckPasswordSignInAsync(user, loginUser.Password, false);

                if (result.Succeeded)
                {
                    var authModel = new AuthenticatedUserModel
                    {
                        Token = _tokenService.CreateToken(user)
                    };

                    Response.Headers.Add("api-key", authModel.Token);

                    return Ok();
                }

                return BadRequest("Niepowodzenie logowania");
            }
            catch
            {
                return BadRequest("Niepowodzenie logowania");
            }
        }

        [HttpGet("orders")]
        [Authorize(Roles = "deliverer")]
        public async Task<ActionResult<DeliveryItemModel[]>> GetOrdersToDeliver()
        {
            try
            {
                var statusPrepared = await _ordersService.GetOrders(new GetOrdersQueryModel() { OrderStatus = OrderStatus.Prepared.ToString() });
                var deliveryDetailsDTO = statusPrepared.Select(o => _mapper.Map<DeliveryItemModel>(o));
                return Ok(deliveryDetailsDTO.ToArray());
            }
            catch
            {
                return BadRequest("Niepowodzenie pobierania");
            }
        }

        [HttpPost("orders/{orderID}/deliver")]
        [Authorize(Roles = "deliverer")]
        public async Task<ActionResult> FinishDelivery(string orderID)
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
