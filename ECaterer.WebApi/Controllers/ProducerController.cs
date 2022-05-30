using AutoMapper;
using ECaterer.Contracts;
using ECaterer.Contracts.Client;
using ECaterer.Contracts.Orders;
using ECaterer.Contracts.Producer;
using ECaterer.Core.Models;
using ECaterer.Core.Models.Enums;
using ECaterer.WebApi.Common.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECaterer.WebApi.Controllers
{
    [Route("producer")]
    [ApiController]
    [Authorize]
    public class ProducerController : ControllerBase
    {
        private readonly IOrdersService _ordersService;
        private readonly Mapper _mapper;

        public ProducerController(IOrdersService ordersService)
        {
            _ordersService = ordersService;
            var mappingConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Address, AddressModel>();
                cfg.CreateMap<DeliveryDetails, DeliveryDetailsModel>();
                cfg.CreateMap<Complaint, ComplaintModel>()
                    .ForMember(dest => dest.Status, opt => opt.MapFrom(col => ((ComplaintStatus)col.Status).ToString()));
                cfg.CreateMap<Order, OrderProducerModel>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(col => col.OrderId));
            });
            _mapper = new Mapper(mappingConfig);
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginUserModel loginUser)
        {
            return Redirect("/client/login");
        }

        [HttpPost("orders/{orderId}/complete")]
        [Authorize(Roles = "producer")]
        public async Task<IActionResult> CompleteOrder([FromRoute] string orderId)
        {
            try
            {
                var (exist, completed) = await _ordersService.PayOrder(orderId);

                if (!exist)
                    return NotFound("Podane zamówienie nie istnieje");

                if (!completed)
                    return BadRequest("Niepowodzenie potwierdzenia wykoniania zamówienia");

                return Ok("Potwierdzenie wykonania zamówienia");
            }
            catch
            {
                return BadRequest("Niepowodzenie potwierdzenia wykoniania zamówienia");
            }
        }

        [HttpPost("orders/{complaintId}/answer-complaint")]
        [Authorize(Roles = "producer")]
        public async Task<IActionResult> AnswerComplaint([FromRoute] Guid complaintId/*, [FromBody] AnswerComplaintModel model*/)
        {
            return BadRequest("Zapisanie nie powiodło się");
        }

        [HttpGet("orders/complaints")]
        [Authorize(Roles = "producer")]
        public async Task<IActionResult> GetOrdersComplaints()
        {
            return BadRequest();
        }

        [HttpGet("orders")]
        [Authorize(Roles = "producer")]
        public async Task<ActionResult<OrderProducerModel[]>> GetOrders([FromQuery] GetOrdersProducerQueryModel getOrdersQuery)
        {
            try
            {
                var orders = (await _ordersService.GetOrders(getOrdersQuery));
                if (orders == null)
                    return BadRequest("Pobranie nie powiodło się");

                var ordersModel = orders
                    .Select(order => _mapper.Map<OrderProducerModel>(order))
                    .ToArray();

                return Ok(ordersModel);
            }
            catch
            {
                return BadRequest("Pobranie nie powiodło się");
            }
        }
    }
}
