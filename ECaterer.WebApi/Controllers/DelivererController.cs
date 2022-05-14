using AutoMapper;
using ECaterer.Contracts;
using ECaterer.Contracts.Client;
using ECaterer.Contracts.Orders;
using ECaterer.Core;
using ECaterer.Core.Models;
using ECaterer.WebApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace ECaterer.WebApi.Controllers
{
    [Route("api/deliverer")]
    [ApiController]
    public class DelivererController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly TokenService _tokenService;
        private readonly DataContext _context;
        private readonly Mapper _mapper;

        public DelivererController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, TokenService tokenService, DataContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _context = context;

            var mapperConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<Address, AddressModel>();
                config.CreateMap<DeliveryDetails, DeliveryDetailsModel>()
                .ForMember(
                    dest => dest.Address,
                    act => act.MapFrom(src => _mapper.Map<AddressModel>(src.Address)));
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
        //[Authorize(Roles = "deliverer")]
        public async Task<ActionResult<DeliveryDetailsModel[]>> GetOrdersToDeliver()
        {
            try
            {
                var StatusPrepared = _context.OrderStatusEnum.FirstOrDefault(status => status.OrderStatusValue == "Prepared");
                if (StatusPrepared is null) return BadRequest();
                var deliveryDetails = _context.Orders.Where(order => order.Status == StatusPrepared).Select(order => order.DeliveryDetails);
                var deliveryDetailsDTO = deliveryDetails.Select(dd => _mapper.Map<DeliveryDetails>(dd));
                return Ok(deliveryDetailsDTO.ToArray());
            }
            catch
            {
                return BadRequest("Niepowodzenie pobierania");
            }
        }

        [HttpPost("orders/{orderID}/deliver")]
        //[Authorize(Roles = "deliverer")]
        public async Task<ActionResult> FinishDelivery(string orderID)
        {
            try
            {
                var order = _context.Orders.FirstOrDefault(order => order.OrderId == orderID);
                if (order is null) 
                    return NotFound("Podane zamówienie nie istnieje");
                var StatusDelivered = _context.OrderStatusEnum.FirstOrDefault(status => status.OrderStatusValue == "Delivered");
                order.Status = StatusDelivered;
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
