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

        [HttpGet("GetMeals")]
        public async Task<ActionResult<IEnumerable<MealDTO>>> GetMeals()
        {
            string token = Request.Cookies["SESSIONID"];
            _apiClient.DefaultRequestHeaders.Add("Authorization", "bearer " + token);
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

        [HttpGet("GetMealsInDiet/{dietId}")]
        public async Task<ActionResult<IEnumerable<MealDTO>>> GetMeals(int dietId)
        {
            return Ok(new List<MealDTO>()
            {
                new MealDTO()
                {
                    Id = "1",
                    Name = "Tort orzechowy",
                    IngredientList = new string[]{"cukier", "mąka", "orzechy" },
                    AllergentList = new string[]{"orzechy", "laktoza" },
                    Calories = 1000,
                    Vegan = true
                },
                new MealDTO()
                {
                    Id = "2",
                    Name = "Tort orzechowy3",
                    IngredientList = new string[]{"cukier", "mąka", "orzechy" },
                    AllergentList = new string[]{"orzechy", "laktoza" },
                    Calories = 1000,
                    Vegan = true
                },
                new MealDTO()
                {
                    Id = "3",
                    Name = "Tort orzechowy4",
                    IngredientList = new string[]{"cukier", "mąka", "orzechy" },
                    AllergentList = new string[]{"orzechy", "laktoza" },
                    Calories = 1500,
                    Vegan = true
                },
                new MealDTO()
                {
                    Id = "4",
                    Name = "Tort orzechowy5",
                    IngredientList = new string[]{"cukier", "mąka", "orzechy" },
                    AllergentList = new string[]{"orzechy", "laktoza" },
                    Calories = 1300,
                    Vegan = false
                },
                new MealDTO()
                {
                    Id = "5",
                    Name = "Tort orzechowy6",
                    IngredientList = new string[]{"cukier", "mąka", "orzechy" },
                    AllergentList = new string[]{"orzechy", "laktoza" },
                    Calories = 1400,
                    Vegan = false
                }
            });
        }
    }
}
