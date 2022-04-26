using ECaterer.Contracts.Orders;
using ECaterer.Core;
using ECaterer.WebApi.Common.Exceptions;
using ECaterer.WebApi.Common.Queries;
using ECaterer.WebApi.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECaterer.WebApi.Controllers
{
    public class DientController : Controller
    {
        DietControllerService _service;
        public DientController(DietControllerService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            return View();
        }

        // GET: api/<DientController>
        [HttpGet]
        public async Task<ActionResult<DietModel[]>> GetDiets([FromQuery] DietQuery query)
        {
            try
            {
                var diets = await _service.GetDiets(query.Offset, query.Limit, query.Sort, query.Name, query.NameWith, 
                                                    query.Vegan, query.Calories, query.CaloriesLt, query.CaloriesHt, 
                                                    query.Price, query.PriceLt, query.PriceHt);

                return Ok(diets);
            }
            catch
            {
                throw new Exception("Niepowodzenie pobrania diet");
            }
        }

        // GET api/<DientController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DietModel>> Get(int id)
        {
            try
            {
                var diet = await _service.GetDietByID(id);
                return Ok(diet);
            }
            catch
            {
                return BadRequest("Niepowodzenie pobrania diety");
            }
        }

        [HttpPost]
        //[Authorize(Roles = "producer")]
        public async Task<ActionResult> Post([FromBody] CreateDietMealsModel dietInfo)
        {
            try
            {
                await _service.AddDiet(dietInfo);
                return Ok("Powodzenie dodania diety");
            }
            catch
            {
                return BadRequest("Niepowodzenie dodania diety");
            }
        }

        [HttpPut("{dietId}")]
        //[Authorize(Roles = "producer")]
        public async Task<ActionResult> EditMeal(int dietId, [FromBody] CreateDietMealsModel dietInfo)
        {
            try
            {
                await _service.EditDiet(dietId, dietInfo);
                return Ok("Powodzenie edycji diety");
            }
            catch (UnexistingDietException)
            {
                return NotFound("Podana dieta nie istnieje");
            }
            catch
            {
                return BadRequest("Niepowodzenie edycji diety");
            }
        }

        // DELETE api/<DientController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _service.DeleteDiet(id);
                return Ok("Powodzenie usunięcia diety");
            }
            catch (UnexistingDietException)
            {
                return NotFound("Podana dieta nie istnieje");
            }
            catch
            {
                return BadRequest("Niepowodzenie usunięcia diety");
            }
        }
    }

    public class CreateDietMealsModel
    {
        public string Name { get; set; }
        public string[] MealsId { get; set; }
        public int Price { get; set; }
    }



}
