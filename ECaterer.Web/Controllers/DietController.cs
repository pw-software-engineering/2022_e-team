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
            var response = await _apiClient.GetAsync("api/diets");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadFromJsonAsync<DietModel[]>();
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
    }
}
