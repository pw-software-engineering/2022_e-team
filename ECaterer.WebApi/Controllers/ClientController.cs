using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECaterer.WebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ECaterer.Core.Models;
using ECaterer.WebApi.Data;
using ECaterer.Core;
using ECaterer.Contracts;
using System.Net;
using ECaterer.Contracts.Client;
using ECaterer.Contracts.Orders;
using ECaterer.WebApi.Common.Interfaces;
using System.Security.Claims;
using ECaterer.Contracts.Meals;
using ECaterer.Core.Models.Enums;
using AutoMapper;

namespace ECaterer.WebApi.Controllers
{
    [Route("client")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly TokenService _tokenService;
        private readonly DataContext _context;

        private readonly IOrderService _orderService;
        private readonly Mapper _mapper;

        public ClientController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, TokenService tokenService, DataContext context, IOrderService ordersService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _context = context;
            _orderService = ordersService;

            var mappingConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Address, AddressModel>();
                cfg.CreateMap<DeliveryDetails, DeliveryDetailsModel>();
                cfg.CreateMap<Complaint, ComplaintModel>()
                    .ForMember(dest => dest.Status, opt => opt.MapFrom(col => ((ComplaintStatus)col.Status).ToString()));
                cfg.CreateMap<Order, OrderClientModel>()
                    .ForMember(dest => dest.Status, opt => opt.MapFrom(col => ((OrderStatus)col.Status).ToString()))
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(col => col.OrderId));
            });
            _mapper = new Mapper(mappingConfig);
        }

        [Route("login")]
        [HttpPost]
        public async Task<ActionResult> Login([FromBody] LoginUserModel loginUser)
        {
            var user = await _userManager.FindByEmailAsync(loginUser.Email);

            if (user == null)
                return BadRequest();
            var Token = _tokenService.CreateToken(user);
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

            return BadRequest();
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(ClientModel registerUser)
        {
            if (_userManager.Users.Any(x => x.Email == registerUser.Email))
            {
                return BadRequest("Email taken");
            }

            var user = new IdentityUser
            {
                Email = registerUser.Email,
                UserName = registerUser.Email
            };

            var result = await _userManager.CreateAsync(user, registerUser.Password);

            if (result.Succeeded)
            {
                var newAddress = new Address()
                {
                    Street = registerUser.Address.Street,
                    BuildingNumber = registerUser.Address.BuildingNumber,
                    ApartmentNumber = registerUser.Address.ApartmentNumber,
                    PostCode = registerUser.Address.PostCode,
                    City = registerUser.Address.City
                };

                _context.Addresses.Add(newAddress);

                _context.Clients.Add(new Client()
                {
                    Name = registerUser.Name,
                    LastName = registerUser.LastName,
                    Email = registerUser.Email,
                    PhoneNumber = registerUser.PhoneNumber,
                    Address = newAddress
                });


                _context.SaveChanges();
                var Token = _tokenService.CreateToken(user);

                Response.Headers.Add("api-key", Token);
                return Ok();
            }

            return BadRequest("Problem registering user");
        }

        [Route("account")]
        [HttpGet]
        [Authorize(Roles = "client")]
        public ActionResult<ClientModel> GetAccount()
        {
            var email = this.User.FindFirstValue(ClaimTypes.Email);

            if (email != null)
            {
                return Ok(_context.Clients.Where(client => client.Email == email).Include(c => c.Address).FirstOrDefault());
            }

            return Unauthorized();
        }

        [Route("account")]
        [HttpPut]
        [Authorize(Roles = "client")]
        public ActionResult UpdateAccount(ClientModel client)
        {
            var email = this.User.FindFirstValue(ClaimTypes.Email);

            if (email != null)
            {
                var old = _context.Clients.Where(client => client.Email == email).Include(c => c.Address).FirstOrDefault();
                var oldAddress = old.Address;

                _context.Entry(old).CurrentValues.SetValues(client);
                _context.SaveChanges();

                // Update - children separately
                if (old != null)
                {
                    if (client.Address == null)
                    {
                        old.Address = null;
                    }
                    else if (oldAddress != null)
                    {
                        _context.Entry(oldAddress).CurrentValues.SetValues(client.Address);

                    }
                    else if (client.Address != null)
                    {
                        old.Address = new Address
                        {
                            Street = client.Address.Street,
                            BuildingNumber = client.Address.BuildingNumber,
                            ApartmentNumber = client.Address.ApartmentNumber,
                            PostCode = client.Address.PostCode,
                            City = client.Address.City
                        };
                    }
                    _context.SaveChanges();
                    return Ok();
                }
                return StatusCode(400);
            }
            return Unauthorized();
        }

        [HttpGet("orders")]
        [Authorize(Roles = "client")]
        public async Task<ActionResult<OrderClientModel[]>> GetOrders([FromQuery] GetOrdersClientQueryModel getOrdersQuery)
        {
            var orders = (await _orderService.GetOrders(getOrdersQuery));
            if (orders == null)
                return BadRequest("Pobranie nie powiodło się");

            var ordersModel = orders
                .Select(order => _mapper.Map<OrderClientModel>(order))
                .ToArray();

            return Ok(ordersModel);
        }

        [HttpPost("orders")]
        [Authorize(Roles = "client")]
        public async Task<IActionResult> AddOrder(AddOrderModel model)
        {
            var email = this.User.FindFirstValue(ClaimTypes.Email);
            var userId = (await _context.Clients.FirstOrDefaultAsync(c => c.Email == email)).ClientId;

            var order = await _orderService.AddOrder(userId, model);

            if (order is null)
                return BadRequest("Zapisanie nie powiodło się");

            return CreatedAtAction(nameof(AddOrder), order.OrderId);

        }

        [HttpPost("orders/{orderId}/pay")]
        [Authorize(Roles = "client")]
        public async Task<IActionResult> PayOrder(string orderId)
        {
            var (exist, paid) = await _orderService.PayOrder(orderId);

            if (!exist)
                return NotFound("Podane zamówienie nie istnieje");

            if (!paid)
                return BadRequest("Opłacenie zamówienia nie powiodło się");

            return Ok("Opłacono zamówienie");
        }
    }
}
