using ECaterer.Contracts;
using ECaterer.Core;
using ECaterer.Core.Models;
using ECaterer.Web.DTO.MealsDTO;
using ECaterer.Web.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ECaterer.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MealsController : Controller
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        private readonly ApiClient _apiClient;

        public MealsController(DataContext context, IConfiguration configuration, ApiClient apiClient)
        {
            _context = context;
            _configuration = configuration;
            _apiClient = apiClient;
        }

        [HttpPost("GetMeals")]
        public async Task<ActionResult<IEnumerable<MealDTO>>> GetMeals()
        {
            var response = await _apiClient.GetAsync("/api/Meals");
            var content = await response.Content.ReadFromJsonAsync<IEnumerable<MealDTO>>();

            if (response.IsSuccessStatusCode)
            {
                return Ok(content);
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return Unauthorized();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
