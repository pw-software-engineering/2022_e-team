using ECaterer.Core;
using ECaterer.Core.Models;
using ECaterer.Web.DTO.DealDTO;
using ECaterer.WebApi.Common.Exceptions;
using ECaterer.WebApi.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECaterer.WebApi.Services
{
    public class DietControllerService: IDietController
    {
        DataContext _context;
        public DietControllerService(DataContext context)
        {
            _context = context;
        }


        public async Task<Diet> GetDietByID(int id)
        {
            var diet = await _context.Diets.FirstOrDefaultAsync(diet => diet.DietId == id);
            if (diet is null)
                throw new UnexistingDietException(id);
            
            return diet;
        }

        public async Task<IEnumerable<Diet>> GetDiets(int? offset, int? limit, string sort, string name, string name_with,
                                                bool? vegan, int? calories, int? colories_lt, int? colories_ht,
                                                int? price, int? price_lt, int? price_ht)
        {
            var diets = await _context.Diets.ToListAsync();
            if (sort is not null)
                switch (sort)
                {
                    case "title(asc)":
                        diets = diets.OrderBy(d => d.Title).ToList();
                        break;
                    case "title(desc)":
                        diets = diets.OrderByDescending(d => d.Title).ToList();
                        break;
                    case "calories(asc)":
                        diets = diets.OrderBy(d => d.Calories).ToList();
                        break;
                    case "calories(desc)":
                        diets = diets.OrderByDescending(d => d.Calories).ToList();
                        break;
                    case "price(asc)":
                        diets = diets.OrderBy(d => d.Price).ToList();
                        break;
                    case "price(desc)":
                        diets = diets.OrderByDescending(d => d.Price).ToList();
                        break;
                    default:
                        throw new Exception("Unexpected sort type");
                }

            if (name is not null)
                diets = diets.Where(d => d.Title.Equals(name)).ToList();
            if (name_with is not null)
                diets = diets.Where(d => d.Title.Contains(name_with)).ToList();
            if (vegan is not null)
                diets = diets.Where(d => d.Vegan == vegan).ToList();
            if (calories is not null)
                diets = diets.Where(d => d.Calories == calories).ToList();
            if (colories_lt is not null)
                diets = diets.Where(d => d.Calories <= colories_lt).ToList();
            if (colories_ht is not null)
                diets = diets.Where(d => d.Calories >= colories_ht).ToList();
            if (price is not null)
                diets = diets.Where(d => d.Price == price).ToList();
            if (price_lt is not null)
                diets = diets.Where(d => d.Price <= price_lt).ToList();
            if (price_ht is not null)
                diets = diets.Where(d => d.Price >= price_ht).ToList();
            if (offset is not null)
                diets = diets.Skip((int)offset).ToList();
            if (limit is not null)
                diets = diets.Take((int)limit).ToList();

            return diets;
        }

    }
}
