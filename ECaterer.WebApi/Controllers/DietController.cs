using AutoMapper;
using ECaterer.Contracts.Diets;
using ECaterer.Contracts.Meals;
using ECaterer.Core;
using ECaterer.Core.Models;
using ECaterer.WebApi.Common.Exceptions;
using ECaterer.WebApi.Common.Interfaces;
using ECaterer.WebApi.Common.Queries;
using ECaterer.WebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECaterer.WebApi.Controllers
{
    [ApiController]
    public class DietController : Controller
    {
        private readonly IDietRepository _diets;
        private readonly Mapper _mapper;


        public DietController(IDietRepository diets)
        {
            _diets = diets;

            var mappingConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Diet, DietModel>()
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(col => col.Title))
                    .ForMember(dest => dest.MealIds, opt => opt.MapFrom(col => col.Meals.Select(m => m.MealId).ToArray()));
                cfg.CreateMap<Diet, GetDietsModel>()
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(col => col.Title))
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(col => col.DietId));
            });
            _mapper = new Mapper(mappingConfig);
        }


        [HttpGet("diets")]
        [Authorize(Roles = "producer,client")]
        public async Task<ActionResult<GetDietsModel[]>> GetDiets([FromQuery] GetDietsQueryModel query)
        {
            var diets = await _diets.GetDiets(query);
            if (diets is null)
                return BadRequest("Niepowodzenie pobrania diet");
            var dietsDTO = diets.Select(diet => _mapper.Map<GetDietsModel>(diet)).ToList();
            return Ok(dietsDTO);
        }

        [HttpGet("diets/{dietId}")]
        [Authorize(Roles = "producer,client")]
        public async Task<ActionResult<DietModel>> GetDietByID(string dietId)
        {
            Diet diet;
            try
            {
                diet = await _diets.GetDietById(dietId);
            }
            catch
            {
                return BadRequest("Niepowodzenie pobrania diety");
            }
            if (diet is null)
                return NotFound("Podana dieta nie istnieje");
            return Ok(_mapper.Map<DietModel>(diet));
        }

        [HttpPost("diets")]
        [Authorize(Roles = "producer")]
        public async Task<ActionResult> AddDiet([FromBody] DietModel dietModel)
        {
            var diet = await _diets.AddDiet(dietModel);
            if (diet is null)
                return BadRequest("Niepowodzenie dodania diety"); ;
            return Ok("Powodzenie dodania diety");
        }

        [HttpPut("diets/{dietId}")]
        [Authorize(Roles = "producer")]
        public async Task<ActionResult> EditDiet(string dietId, [FromBody] DietModel dietModel)
        {
            var diet = await _diets.EditDiet(dietId, dietModel);
            if (diet is null)
                return BadRequest("Niepowodzenie edycji diety");
            return Ok("Powodzenie edycji diety");
        }

        [HttpDelete("diets/{dietId}")]
        [Authorize(Roles = "producer")]
        public async Task<ActionResult> DeleteDiet(string dietId)
        {
            Diet diet;
            try
            {
                diet = await _diets.DeleteDiet(dietId);
            }
            catch
            {
                return BadRequest("Niepowodzenie usunięcia diety");
            }
            if (diet is null)
                return NotFound("Podana dieta nie istnieje");
            return Ok("Powodzenie usunięcia diety");
        }
    }
}
