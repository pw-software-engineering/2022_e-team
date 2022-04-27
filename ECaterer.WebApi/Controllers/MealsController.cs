using AutoMapper;
using ECaterer.Contracts.Meals;
using ECaterer.Core;
using ECaterer.Core.Models;
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
    [Route("api/meals")]
    [ApiController]
    //[Authorize]
    public class MealsController : ControllerBase
    {
        private readonly IMealRepository _meals;
        private readonly Mapper _mapper;

        public MealsController(IMealRepository meals)
        {
            _meals = meals;
            var mappingConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Meal, MealModel>()
                    .ForMember(dest => dest.AllergentList, opt => opt.MapFrom(col => col.AllergentList.Select(al => al.Name).ToList()))
                    .ForMember(dest => dest.IngredientList, opt => opt.MapFrom(col => col.IngredientList.Select(ing => ing.Name).ToList()));
                cfg.CreateMap<Meal, GetMealsResponseModel>();
            });
            _mapper = new Mapper(mappingConfig);
        }

        [HttpGet]
        //[Authorize(Roles = "producer, client")]
        public async Task<ActionResult<GetMealsResponseModel[]>> GetMeals([FromQuery] GetMealsQueryModel query)
        {
            try
            {
                var meals = await _meals.GetMeals(query.Offset, query.Limit, query.Sort, query.Name, query.Name_with, query.Vegan, query.Calories, query.Calories_lt, query.Calories_ht);
                var mealsDTO = meals.Select(meal => _mapper.Map<GetMealsResponseModel>(meal)).ToList();
                return Ok(mealsDTO);
            }
            catch
            {
                return BadRequest("Niepowodzenie pobrania danych posiłków");
            }
        }

        [HttpPost]
        //[Authorize(Roles = "producer")]
        public async Task<ActionResult> AddMeal([FromBody] MealModel mealDTO)
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
        public async Task<ActionResult<MealModel>> GetMealById(string mealId)
        {
            try
            {
                var meal = await _meals.GetMealById(mealId);
                return Ok(_mapper.Map<MealModel>(meal));
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
        public async Task<ActionResult> EditMeal(string mealId, [FromBody] MealModel mealModel)
        {
            try
            {
                await _meals.EditMeal(mealId, mealModel);
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
        public async Task<ActionResult> DeleteMeal(string mealId)
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
