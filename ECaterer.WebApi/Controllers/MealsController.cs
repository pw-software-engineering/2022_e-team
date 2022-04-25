using ECaterer.Core;
using ECaterer.Core.Models;
using ECaterer.Web.DTO.MealsDTO;
using ECaterer.WebApi.Common.Exceptions;
using ECaterer.WebApi.Common.Interfaces;
using ECaterer.WebApi.Common.Queries;
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
        private readonly IMealRepository _meals;

        public MealsController(IMealRepository meals)
        {
            _meals = meals;
        }

        [HttpGet]
        //[Authorize(Roles = "producer, client")]
        public async Task<ActionResult<GetMealDTO[]>> GetMeals([FromQuery] GetMealsQuery query)
        {
            try
            {
                var meals = await _meals.GetMeals(query.Offset, query.Limit, query.Sort, query.Name, query.Name_with, query.Vegan, query.Calories, query.Calories_lt, query.Calories_ht);
                var mealsDTO = meals.Select(meal => new GetMealDTO(meal)).ToList();
                return Ok(mealsDTO);
            }
            catch
            {
                return BadRequest("Niepowodzenie pobrania danych posiłków");
            }
        }

        [HttpPost]
        //[Authorize(Roles = "producer")]
        public async Task<ActionResult> AddMeal([FromBody] MealDTO mealDTO)
        {
            try
            {
                await _meals.AddMeal(mealDTO);
                return Ok("Powodzenie dodania posiłku");
            }
            catch
            {
                return BadRequest("Niepowodzenie dodania posiłku");
            }
        }

        [HttpGet("{mealId}")]
        //[Authorize(Roles = "producer, client")]
        public async Task<ActionResult<MealDTO>> GetMealById(int mealId)
        {
            try
            {
                var meal = await _meals.GetMealById(mealId);
                return Ok(new MealDTO(meal));
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
        //[Authorize(Roles = "producer")]
        public async Task<ActionResult> EditMeal(int mealId, [FromBody] MealDTO mealDTO)
        {
            try
            {
                await _meals.EditMeal(mealId, mealDTO);
                return Ok("Powodzenie edycji posiłku");
            }
            catch (UnexistingMealException)
            {
                return NotFound("Podany posiłek nie istnieje");
            }
            catch
            {
                return BadRequest("Niepowodzenie edycji posiłku");
            }
        }

        [HttpDelete("{mealId}")]
        //[Authorize(Roles = "producer")]
        public async Task<ActionResult> DeleteMeal(int mealId)
        {
            try
            {
                await _meals.DeleteMeal(mealId);
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
