using ECaterer.Core;
using ECaterer.Core.Models;
using ECaterer.Web.DTO.MealsDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECaterer.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MealsController : ControllerBase
    {
        private readonly DataContext _context;

        public MealsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public Task<ActionResult<GetMealDTO[]>> GetMeals(int offset = 0, int limit = 10, string sort = "title(asc)", string name = null, string name_with = null, bool vegan = false, int colories = 0, int colories_lt = 0, int colories_ht = 0)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public Task<ActionResult> PostMeal([FromBody] PostMealDTO mealDTO)
        {
            throw new NotImplementedException();
        }

        [HttpGet("{mealId}")]
        public Task<ActionResult<Meal>> GetMealById(string mealId)
        {
            throw new NotImplementedException();
        }

        [HttpPut("{mealId}")]
        public Task<ActionResult<Meal>> EditMeal(string mealId, [FromBody] Meal meal)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{mealId}")]
        public Task<ActionResult> DeleteMeal(string mealId)
        {
            throw new NotImplementedException();
        }
    }
}
