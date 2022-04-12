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
        public async Task<ActionResult<Meal>> GetMealById(int mealId)
        {
            try
            {
                var meal = _context.Meals.FirstOrDefault(meal => meal.MealId == mealId);
                if (meal is null)
                    return NotFound("Podany posiłek nie istnieje");

                return Ok(meal);
            }
            catch
            {
                return BadRequest("Niepowodzenie pobrania posiłku");
            }
        }

        [HttpPut("{mealId}")]
        public Task<ActionResult<Meal>> EditMeal(string mealId, [FromBody] Meal meal)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{mealId}")]
        public async Task<ActionResult> DeleteMeal(int mealId)
        {
            try
            {
                var meal = _context.Meals.FirstOrDefault(meal => meal.MealId == mealId);
                if (meal is null)
                    return NotFound("Podany posiłek nie istnieje");

                _context.Meals.Remove(meal);
                await _context.SaveChangesAsync();

                return Ok("Powodzenie usunięcia posiłku");
            }
            catch
            {
                return BadRequest("Niepowodzenie usunięcia posiłku");
            }
        }
    }
}
