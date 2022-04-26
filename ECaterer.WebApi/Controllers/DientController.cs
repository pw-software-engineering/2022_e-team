using ECaterer.Core;
using ECaterer.Web.DTO.DealDTO;
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
        public async Task<ActionResult<DietDTO[]>> GetDiets([FromQuery] DietQuery query)
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

            }
        }

        // GET api/<DientController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DietDTO>> Get(int id)
        {
            try
            {
                var diet = await _service.GetDietByID(id);
                return Ok(new DietDTO(diet));
            }
            catch
            {
                return BadRequest("Niepowodzenie pobrania diety");
            }
        }

        // POST api/<DientController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<DientController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<DientController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }




}
