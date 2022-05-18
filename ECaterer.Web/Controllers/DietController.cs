using ECaterer.Core.Models;
using ECaterer.Web.DTO;
using ECaterer.Web.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Text.Json;
using ECaterer.Contracts.Orders;
using ECaterer.Web.Converters;
using System.Web;
using System.Collections.Generic;
using System.Linq;

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
            var query = HttpUtility.ParseQueryString(String.Empty);
            query["offset"] = offset.ToString();
            query["limit"] = limit.ToString();
            query["sort"] = sort;
            query["startDate"] = startDate.ToString();
            query["endDate"] = endDate.ToString();
            query["price"] = price.ToString();
            query["price_lt"] = price_lt.ToString();
            query["price_ht"] = price_ht.ToString();

            var response = await _apiClient.GetAsync("api/diets" + query.ToString());

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadFromJsonAsync<DietModel[]>();
                return Ok(content.Select(diet => DietConverter.ConvertBack(diet)).ToArray());
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
            var response = await _apiClient.GetAsync("api/diets");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadFromJsonAsync<DietModel[]>();
                return Ok(content.Select(diet => ProducerDietConverter.ConvertBack(diet)).ToArray());
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
            var response = await _apiClient.GetAsync($"api/diets/{dietId}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadFromJsonAsync<DietModel>();
                return Ok(EditDietConverter.ConvertBack(content));
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
            var response = await _apiClient.DeleteAsync($"api/diets/{dietId}");

            if (response.IsSuccessStatusCode)
            {
                return Ok();
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

        [HttpPost("editDiet")]
        public async Task<ActionResult> EditDiet([FromBody]SaveDietDTO model)
        {
            // Need to be discussed
            // create diet
            if (model.Id == "0")
            {
                return Ok();
            }
            // edit diet
            return Ok();
        }

        [HttpPut("getDietsWithIds")]
        public async Task<ActionResult<DietDTO[]>> GetDietsWithIds([FromBody]IEnumerable<string> dietIds)
        {
            var diets = new List<DietDTO>();
            foreach (var dietId in dietIds)
            {
                var response = await _apiClient.GetAsync($"api/diets/{dietId}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadFromJsonAsync<DietModel>();
                    diets.Add(DietConverter.ConvertBack(content));
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
            return Ok(diets.ToArray());
        }
    }
}
