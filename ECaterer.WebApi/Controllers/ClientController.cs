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
                return Ok(new AuthenticatedUserModel
                {
                    //UserName = user.UserName,
                    Token = _tokenService.CreateToken(user)
                });
            }

            return Unauthorized();
        }

        [HttpPost("Register")]
        public async Task<ActionResult<AuthenticatedUserModel>> Register(RegisterUserModel registerUser)
        {
            if (_userManager.Users.Any(x => x.Email == registerUser.Client.Email))
            {
                return BadRequest("Email taken");
            }

            var user = new IdentityUser
            {
                Email = registerUser.Client.Email,
                UserName = registerUser.Client.Email
            };

            var result = await _userManager.CreateAsync(user, registerUser.Password);

            if (result.Succeeded)
            {

                _context.Clients.Add(registerUser.Client);
                _context.SaveChanges();

                return Ok(new AuthenticatedUserModel
                {
                    //UserName = user.UserName,
                    Token = _tokenService.CreateToken(user)
                });
            }

            return BadRequest("Problem registering user");
        }
    }
}
