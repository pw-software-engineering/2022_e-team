using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECaterer.WebApi.Models;
using ECaterer.WebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ECaterer.Core.Models;
using ECaterer.WebApi.Data;
using ECaterer.Core;

namespace ECaterer.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly TokenService _tokenService;
        private readonly DataContext _context;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, TokenService tokenService, DataContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _context = context;
        }

        [Route("Login")]
        [HttpPost]
        public async Task<ActionResult<AuthenticatedUserModel>> Login([FromBody] LoginUserModel loginUser)
        {
            var user = await _userManager.FindByEmailAsync(loginUser.Email);

            if (user == null)
                return Unauthorized();

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginUser.Password, false);

            if (result.Succeeded)
            {
                return new AuthenticatedUserModel
                {
                    Token = _tokenService.CreateToken(user),
                   // UserName = user.Email
                };
            }

            return Unauthorized();
        }

        [HttpPost("Register")]
        public async Task<ActionResult<AuthenticatedUserModel>> Register(RegisterUserModel registerUser)
        {
            if (await _userManager.Users.AnyAsync(x => x.Email == registerUser.Email))
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
                var client = new Client
                {
                    Name = registerUser.Name,
                    LastName = registerUser.LastName,
                    Email = registerUser.Email,
                    Address = registerUser.Address,
                    PhoneNumber = registerUser.PhoneNumber
                };

                _context.Clients.Add(client);
                _context.SaveChanges();

                return new AuthenticatedUserModel
                {
                    //UserName = user.UserName,
                    Token = _tokenService.CreateToken(user)
                };

            }

            return BadRequest("Problem registering user");
        }
    }
}
