using ECaterer.Core;
using ECaterer.Core.Models;
using ECaterer.Web.DTO.MealsDTO;
using ECaterer.WebApi.Common.Exceptions;
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
    [Route("api/[controller]")]
    [ApiController]
    public class MealsController : ControllerBase
    {
        private readonly IMealRepository _meals;

        public MealsController(IMealRepository meals)
        {
            _meals = meals;
        }

        [HttpGet]
        [Authorize(Roles = "producer, client")]
        public Task<ActionResult<GetMealDTO[]>> GetMeals(int offset = 0, int limit = 10, string sort = "title(asc)", string name = null, string name_with = null, bool vegan = false, int colories = 0, int colories_lt = 0, int colories_ht = 0)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Authorize(Roles = "producer")]
        public Task<ActionResult> PostMeal([FromBody] AddMealDTO mealDTO)
        {
            throw new NotImplementedException();
        }

        [HttpGet("{mealId}")]
        [Authorize(Roles = "producer, client")]
        public async Task<ActionResult<Meal>> GetMealById(int mealId)
        {
            try
            {
                var meal = await _meals.GetMealById(mealId);
                return Ok(meal);
            }
            catch (UnexistingMealException)
            {
                return NotFound("Podany posiłek nie istnieje");
            }
            catch
            {
                return BadRequest("Niepowodzenie pobrania posiłku");
            }
        }

        [HttpPut("{mealId}")]
        [Authorize(Roles = "producer")]
        public Task<ActionResult<Meal>> EditMeal(string mealId, [FromBody] AddMealDTO meal)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{mealId}")]
        [Authorize(Roles = "producer")]
        public async Task<ActionResult> DeleteMeal(int mealId)
        {
            try
            {
                _meals.DeleteMeal(mealId);
                return Ok("Powodzenie usunięcia posiłku");
            }
            catch (UnexistingMealException)
            {
                return NotFound("Podany posiłek nie istnieje");
            }
            catch
            {
                return BadRequest("Niepowodzenie usunięcia posiłku");
            }
        }
    }
}
