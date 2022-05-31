using AutoMapper;
using ECaterer.Contracts;
using ECaterer.Contracts.Client;
using ECaterer.Contracts.Orders;
using ECaterer.Contracts.Producer;
using ECaterer.Core.Models;
using ECaterer.Core.Models.Enums;
using ECaterer.WebApi.Common.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECaterer.WebApi.Controllers
{
    [Route("producer")]
    [ApiController]
    public class ProducerController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IComplaintService _complaintService;
        private readonly ClientController _clientController;
        private readonly Mapper _mapper;

        public ProducerController(IOrderService ordersService, IComplaintService complaintService, ClientController clientController)
        {
            _orderService = ordersService;
            _clientController = clientController;
            _complaintService = complaintService;

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
            _clientController.ControllerContext = new ControllerContext(ControllerContext);
            var response = await _clientController.Login(loginUser);
            ControllerContext = new ControllerContext(_clientController.ControllerContext);
            
            return response;
        }

        [HttpPost("orders/{orderId}/complete")]
        [Authorize/*(Roles = "producer")*/]
        public async Task<IActionResult> CompleteOrder([FromRoute] string orderId)
        {
            try
            {
                var (exist, completed) = await _orderService.CompleteOrder(orderId);

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

        [HttpPost("orders/{orderId}/answer-complaint")]
        [Authorize/*(Roles = "producer")*/]
        public async Task<IActionResult> AnswerComplaint([FromRoute] string orderId, [FromBody] AnswerComplaintModel model)
        {
            try
            {
                var (exist, answered) = await _complaintService.AnswerComplaint(orderId, model.Complaint_answer);

                if (!exist)
                    return NotFound("Podane zamówienie nie istnieje albo nie posiada reklamacji");

                if (!answered)
                    return BadRequest("Zapisanie nie powiodło się");

                return Ok("Zapisano odpowiedź do reklamacji");
            }
            catch
            {
                return BadRequest("Zapisanie nie powiodło się");
            }
        }

        [HttpGet("orders/complaints")]
        [Authorize/*(Roles = "producer")*/]
        public async Task<ActionResult<ComplaintModel[]>> GetOrdersComplaints()
        {
            try
            {
                var complaints = (await _complaintService.GetOrdersComplaints());
                if (complaints is null)
                    return BadRequest("Pobieranie nie powiodło się");

                var complaintsModel = complaints
                    .Select(complaint => _mapper.Map<ComplaintModel>(complaint))
                    .ToArray();

                return Ok(complaintsModel);
            }
            catch
            {
                return BadRequest("Pobieranie nie powiodło się");
            }
        }

        [HttpGet("orders")]
        [Authorize/*(Roles = "producer")*/]
        public async Task<ActionResult<OrderProducerModel[]>> GetOrders([FromQuery] GetOrdersProducerQueryModel getOrdersQuery)
        {
            try
            {
                var orders = (await _orderService.GetOrders(getOrdersQuery));
                if (orders is null)
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
