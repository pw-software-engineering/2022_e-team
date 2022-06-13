using ECaterer.Contracts.Diets;
using ECaterer.Contracts.Meals;
using ECaterer.Web.Converters;
using ECaterer.Web.DTO.MealsDTO;
using ECaterer.Web.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ECaterer.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MealsController : Controller
    {
        private readonly ApiClient _apiClient;

        public MealsController(ApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        //[HttpGet("GetMeals")]
        //public async Task<ActionResult<IEnumerable<MealDTO>>> GetMeals()
        //{
        //    // TODO: You want get AllergentList and IngredientList, but there are no such fields in GetMealsResponseModel

        //    var message = new HttpRequestMessage(HttpMethod.Get, "meals");
        //    TokenPropagator.Propagate(Request, message);
        //    var response = await _apiClient.SendAsync(message);

        //    if (response.IsSuccessStatusCode)
        //    {
        //        var content = await response.Content.ReadFromJsonAsync<GetMealsResponseModel[]>();
        //        var meals = content.Select(meal => MealConverter.ConvertBack(meal)).AsEnumerable();
        //        return Ok(content);
        //    }
        //    else if (response.StatusCode == HttpStatusCode.Unauthorized)
        //    {
        //        return Unauthorized();
        //    }
        //    else
        //    {
        //        return BadRequest();
        //    }
        //}

        [HttpGet("GetMealsInDiet/{dietId}")]
        public async Task<ActionResult<IEnumerable<MealDTO>>> GetMeals(string dietId)
        {
            // TODO: API returns wrong object

            if (dietId == "0")
            {
                return Ok(new List<MealDTO>());
            }

            var message = new HttpRequestMessage(HttpMethod.Get, $"diets/{dietId}");
            TokenPropagator.Propagate(Request, message);
            var response = await _apiClient.SendAsync(message);

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    // TODO: get meals from correct object
                    var diet = await response.Content.ReadFromJsonAsync<DietModel>();
                    return Ok(diet.Meals.Select(meal => MealConverter.ConvertBack(meal)));
                case HttpStatusCode.Unauthorized:
                    return Unauthorized();
                case HttpStatusCode.BadRequest:
                    return BadRequest();
                case HttpStatusCode.NotFound:
                    return NotFound();
                default:
                    return BadRequest();
            }
        }
    }
}
