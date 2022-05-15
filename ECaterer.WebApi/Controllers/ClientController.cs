﻿using System;
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

namespace ECaterer.WebApi.Controllers
{
    [Route("api/client")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly TokenService _tokenService;
        private readonly DataContext _context;

        private readonly IOrdersService _ordersService;

        public ClientController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, TokenService tokenService, DataContext context, IOrdersService ordersService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _context = context;
        }

        [Route("login")]
        [HttpPost]
        public async Task<ActionResult> Login([FromBody] LoginUserModel loginUser)
        {
            var user = await _userManager.FindByEmailAsync(loginUser.Email);

            if (user == null)
                return Unauthorized();

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginUser.Password, false);
            
            if (result.Succeeded)
            {
                var authModel = new AuthenticatedUserModel
                {
                    Token = _tokenService.CreateToken(user)
                };

                Response.Headers.Add("api-key", authModel.Token);

                return Ok();

                //    new AuthenticatedUserModel
                //{
                //    Token = _tokenService.CreateToken(user)
                //});
            }

            return Unauthorized();
        }

        [HttpPost("register")]
        public async Task<ActionResult<AuthenticatedUserModel>> Register(ClientModel registerUser)
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
                _context.Clients.Add(new Client()
                {
                    Name = registerUser.Name,
                    LastName = registerUser.LastName,
                    Email = registerUser.Email,
                    PhoneNumber = registerUser.PhoneNumber,

                    Address = new Address()
                    {
                        Street = registerUser.Address.Street,
                        BuildingNumber = registerUser.Address.BuildingNumber,
                        ApartmentNumber = registerUser.Address.ApartmentNumber,
                        PostCode = registerUser.Address.PostCode,
                        City = registerUser.Address.City
                    }
                }) ;

                _context.SaveChanges();
                var Token = _tokenService.CreateToken(user);

                Response.Headers.Add("api-key", Token);
                return Ok(new AuthenticatedUserModel() { Token = Token });
            }

            return BadRequest("Problem registering user");
        }

        [HttpGet("orders")]
        [Authorize(Roles = "client")]
        public async Task<IActionResult> GetOrders(GetOrdersQueryModel registerUser)
        {
            return BadRequest();
        }

        [HttpPost("orders")]
        [Authorize(Roles = "client")]
        public async Task<IActionResult> AddOrder(ClientModel registerUser)
        {
            return BadRequest();
        }

        [HttpPost("orders/{orderId}/pay")]
        [Authorize(Roles = "client")]
        public async Task<IActionResult> PayOrder(string orderId)
        {
            var (exist, paid) = await _ordersService.PayOrder(orderId);

            if (!exist)
                return NotFound("Podane zamówienie nie istnieje");

            if (!paid)
                return BadRequest("Opłacenie zamówienia nie powiodło się");

            return Ok("Opłacono zamówienie");
        }
    }
}
