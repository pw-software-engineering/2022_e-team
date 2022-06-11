﻿using AutoMapper;
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
using Microsoft.EntityFrameworkCore;
using System;
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
        [Authorize(Roles = "deliverer")]
        public async Task<ActionResult<OrderDelivererModel[]>> GetOrdersToDeliver()
        {
            var ordersWithStatusPrepared = await _ordersService.GetOrders(new GetOrdersDelivererQueryModel());

            if (ordersWithStatusPrepared is null)
                return BadRequest("Niepowodzenie pobierania");

            var deliveryDetailsDTO = ordersWithStatusPrepared.Select(o => _mapper.Map<OrderDelivererModel>(o)).ToArray();
            return Ok(deliveryDetailsDTO);
        }

        [HttpGet("history")]
        [Authorize(Roles = "deliverer")]
        public async Task<ActionResult<HistoryDelivererModel[]>> GetOrdersHistory()
        {
            var ordersWithStatusPrepared = (await _ordersService.GetOrders(new GetHistoryDelivererQueryModel())).ToList();

            if (ordersWithStatusPrepared is null)
                return BadRequest("Niepowodzenie pobierania");

            var deliveryDetailsDTO = ordersWithStatusPrepared.Select(o => _mapper.Map<HistoryDelivererModel>(o)).ToArray();
            return Ok(deliveryDetailsDTO);
        }

        [HttpPost("orders/{orderID}/deliver")]
        [Authorize(Roles = "deliverer")]
        public async Task<ActionResult> FinishDelivery([FromQuery] string orderID)
        {
            var order = _context.Orders.Include(o => o.DeliveryDetails).FirstOrDefault(order => order.OrderId == orderID);
            if (order is null)
                return NotFound("Podane zamówienie nie istnieje");
            if (order.Status != (int)OrderStatus.Prepared)
                return BadRequest("Niepowodzenie potwierdzenia dostawy");
            order.Status = (int)OrderStatus.Delivered;
            order.DeliveryDetails.DeliveryDate = DateTime.Now;
            _context.Orders.Update(order);
            _context.DeliveryDetails.Update(order.DeliveryDetails);
            await _context.SaveChangesAsync();
            return Ok("Powodzenie potwierdzenia dostawy");
        }
    }
}
