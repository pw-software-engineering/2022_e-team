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
using System.Security.Claims;

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

        public ClientController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, TokenService tokenService, DataContext context)
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

                return Ok(authModel);
            }

            return Unauthorized();
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

                var authModel = new AuthenticatedUserModel
                {
                    Token = Token
                };

                Response.Headers.Add("api-key", Token);
                return Ok(authModel);
            }

            return BadRequest("Problem registering user");
        }

        [Route("account")]
        [HttpGet]
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
                    if(client.Address == null)
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
    }
}
