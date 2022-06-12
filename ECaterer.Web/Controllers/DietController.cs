using ECaterer.Contracts.Diets;
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
        // TODO: Do we really need all these arguments as we can filter data on frontend?
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

            var message = new HttpRequestMessage(HttpMethod.Get, "diets" + query.ToString());
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
            // TODO: You want get description, but there is no description in GetDietsModel

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

        [HttpGet("getEditDiets/{id}")]
        public async Task<ActionResult<EditDietDTO>> GetEditDietModel(string dietId)
        {
            // TODO: API returns object different from specification

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

        [HttpPut("deleteDiet/{id}")]
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
            
            // create diet
            if (model.Id == "0")
            {
                var messageCreate = new HttpRequestMessage(HttpMethod.Post, "diets");
                TokenPropagator.Propagate(Request, messageCreate);
                messageCreate.Content = JsonContent.Create(DietConverter.Convert(model));

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
            var message = new HttpRequestMessage(HttpMethod.Put, $"diets/{model.Id}");
            TokenPropagator.Propagate(Request, message);
            message.Content = JsonContent.Create(DietConverter.Convert(model));

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
