using ECaterer.Contracts.Diets;
using ECaterer.Contracts.Meals;
using ECaterer.Web.Converters;
using ECaterer.Web.DTO;
using ECaterer.Web.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Web;

namespace ECaterer.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DietController : Controller
    {
        private readonly ApiClient _apiClient;

        public DietController(ApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        [HttpGet("getDiets")]
        public async Task<ActionResult<DietDTO[]>> GetDiets(
            [FromQuery] int offset = 0,
            [FromQuery] int limit = 0,
            [FromQuery] string sort = "startDate(asc)",
            [FromQuery] DateTime? startDate = null,
            [FromQuery] DateTime? endDate = null,
            [FromQuery] int price = 0,
            [FromQuery] int price_lt = 0,
            [FromQuery] int price_ht = 0)
        {
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["offset"] = offset.ToString();
            query["limit"] = limit.ToString();
            query["sort"] = sort;
            query["startDate"] = startDate.ToString();
            query["endDate"] = endDate.ToString();
            query["price"] = price.ToString();
            query["price_lt"] = price_lt.ToString();
            query["price_ht"] = price_ht.ToString();

            var message = new HttpRequestMessage(HttpMethod.Get, "diets");
            TokenPropagator.Propagate(Request, message);

            var response = await _apiClient.SendAsync(message);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadFromJsonAsync<GetDietsModel[]>();
                var diets = new DietDTO[content.Length];
                for (int i = 0; i < content.Length; i++)
                {
                    diets[i] = DietConverter.ConvertBack(content[i]);
                }
                return Ok(diets);
            }
            else if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return Unauthorized();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("getProducerDiets")]
        public async Task<ActionResult<ProducerDietDTO[]>> GetProducerDiets()
        {
            var message = new HttpRequestMessage(HttpMethod.Get, "diets");
            TokenPropagator.Propagate(Request, message);

            var response = await _apiClient.SendAsync(message);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadFromJsonAsync<GetDietsModel[]>();
                var diets = new ProducerDietDTO[content.Length];
                for (int i = 0; i < content.Length; i++)
                {
                    diets[i] = ProducerDietConverter.ConvertBack(content[i]);
                }
                return Ok(diets);
            }
            else if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return Unauthorized();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("getEditDiets/{dietId}")]
        public async Task<ActionResult<EditDietDTO>> GetEditDietModel(string dietId)
        {
            if (dietId == "0")
            {
                return Ok(new EditDietDTO()
                {
                    Id = "0",
                    Calories = 0,
                    Vegan = true,
                    Description = ""
                });
            }

            var message = new HttpRequestMessage(HttpMethod.Get, $"diets/{dietId}");
            TokenPropagator.Propagate(Request, message);

            var response = await _apiClient.SendAsync(message);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadFromJsonAsync<DietModel>();
                var diet = EditDietConverter.ConvertBack(content);
                return Ok(diet);
            }
            else if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return Unauthorized();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("deleteDiet/{dietId}")]
        public async Task<ActionResult> DeleteDiet(string dietId)
        {
            var message = new HttpRequestMessage(HttpMethod.Delete, $"diets/{dietId}");
            TokenPropagator.Propagate(Request, message);

            var response = await _apiClient.SendAsync(message);

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    return Ok();
                case HttpStatusCode.BadRequest:
                    return BadRequest();
                case HttpStatusCode.NotFound:
                    return NotFound();
                case HttpStatusCode.Unauthorized:
                    return Unauthorized();
                default:
                    return BadRequest();
            }
        }

        [HttpPost("editDiet")]
        public async Task<ActionResult> EditDiet([FromBody]SaveDietDTO model)
        {
            // TODO: To add/edit diet, I need name of one
            
            var mealIds = new List<string>();

            // create diet
            if (model.Id == "0")
            {
                // add all meals
                foreach (var meal in model.Meals)
                {
                    var messageCreateMeal = new HttpRequestMessage(HttpMethod.Post, "meals");
                    TokenPropagator.Propagate(Request, messageCreateMeal);
                    messageCreateMeal.Content = JsonContent.Create(MealConverter.Convert(meal));

                    var responseCreateMeal = await _apiClient.SendAsync(messageCreateMeal);

                    if (responseCreateMeal.StatusCode != HttpStatusCode.OK)
                        return BadRequest();

                    var query = HttpUtility.ParseQueryString(string.Empty);
                    query["name"] = meal.Name;
                    query["vegan"] = meal.Vegan.ToString();
                    query["calories"] = meal.Calories.ToString();

                    var messageGetMeal = new HttpRequestMessage(HttpMethod.Get, "meals?" + query.ToString());
                    TokenPropagator.Propagate(Request, messageGetMeal);

                    var responseGetMeal = await _apiClient.SendAsync(messageGetMeal);

                    if (responseGetMeal.StatusCode != HttpStatusCode.OK)
                        return BadRequest();

                    var content = await responseGetMeal.Content.ReadFromJsonAsync<GetMealsResponseModel[]>();

                    if (content == null || content.Length == 0)
                        return BadRequest();

                    mealIds.Add(content.First().Id);
                }

                // add diet
                var messageCreate = new HttpRequestMessage(HttpMethod.Post, "diets");
                TokenPropagator.Propagate(Request, messageCreate);
                messageCreate.Content = JsonContent.Create(DietConverter.Convert(model, mealIds));

                var responseCreate = await _apiClient.SendAsync(messageCreate);

                switch (responseCreate.StatusCode)
                {
                    case HttpStatusCode.OK:
                        return Ok();
                    case HttpStatusCode.BadRequest:
                        return BadRequest();
                    case HttpStatusCode.Unauthorized:
                        return Unauthorized();
                    default:
                        return BadRequest();
                }
            }

            // edit diet
            foreach (var meal in model.Meals)
            {
                if (int.TryParse(meal.Id, out _))
                {
                    var messageCreateMeal = new HttpRequestMessage(HttpMethod.Post, "meals");
                    TokenPropagator.Propagate(Request, messageCreateMeal);
                    messageCreateMeal.Content = JsonContent.Create(MealConverter.Convert(meal));

                    var responseCreateMeal = await _apiClient.SendAsync(messageCreateMeal);

                    if (responseCreateMeal.StatusCode != HttpStatusCode.OK)
                        return BadRequest();

                    var query = HttpUtility.ParseQueryString(string.Empty);
                    query["name"] = meal.Name;
                    query["vegan"] = meal.Vegan.ToString();
                    query["calories"] = meal.Calories.ToString();

                    var messageGetMeal = new HttpRequestMessage(HttpMethod.Get, "meals?" + query.ToString());
                    TokenPropagator.Propagate(Request, messageGetMeal);

                    var responseGetMeal = await _apiClient.SendAsync(messageGetMeal);

                    if (responseGetMeal.StatusCode != HttpStatusCode.OK)
                        return BadRequest();

                    var content = await responseGetMeal.Content.ReadFromJsonAsync<GetMealsResponseModel[]>();

                    if (content == null || content.Length == 0)
                        return BadRequest();

                    mealIds.Add(content.First().Id);
                }
                else
                {
                    var messageEditMeal = new HttpRequestMessage(HttpMethod.Put, $"meals/{meal.Id}");
                    TokenPropagator.Propagate(Request, messageEditMeal);
                    messageEditMeal.Content = JsonContent.Create(MealConverter.Convert(meal));

                    var responseEditMeal = await _apiClient.SendAsync(messageEditMeal);

                    if (responseEditMeal.StatusCode != HttpStatusCode.OK)
                        return BadRequest();

                    mealIds.Add(meal.Id);
                }
            }

            var message = new HttpRequestMessage(HttpMethod.Put, $"diets/{model.Id}");
            TokenPropagator.Propagate(Request, message);
            message.Content = JsonContent.Create(DietConverter.Convert(model, mealIds));

            var response = await _apiClient.SendAsync(message);

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    return Ok();
                case HttpStatusCode.BadRequest:
                    return BadRequest();
                case HttpStatusCode.Unauthorized:
                    return Unauthorized();
                default:
                    return BadRequest();
            }
        }

        [HttpPut("getDietsWithIds")]
        public async Task<ActionResult<DietDTO[]>> GetDietsWithIds([FromBody]IEnumerable<string> dietIds)
        {
            var message = new HttpRequestMessage(HttpMethod.Get, "diets");
            TokenPropagator.Propagate(Request, message);

            var response = await _apiClient.SendAsync(message);

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var content = await response.Content.ReadFromJsonAsync<GetDietsModel[]>();
                    return Ok(content.Where(diet => dietIds.Contains(diet.Id)).Select(diet => DietConverter.ConvertBack(diet)));
                case HttpStatusCode.BadRequest:
                    return BadRequest();
                case HttpStatusCode.Unauthorized:
                    return Unauthorized();
                default:
                    return BadRequest();
            }
        }
    }
}
