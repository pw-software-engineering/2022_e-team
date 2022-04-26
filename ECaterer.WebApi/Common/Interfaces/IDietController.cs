using ECaterer.Core.Models;
using ECaterer.Web.DTO.DealDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECaterer.WebApi.Common.Interfaces
{
    public interface IDietController
    {
        public Task<Diet> GetDietByID(int id);
        public Task<IEnumerable<Diet>> GetDiets(int? offset, int? limit, string sort, string name, string name_with, 
                                                bool? vegan, int? calories, int? colories_lt, int? colories_ht,
                                                int? price, int? price_lt, int? price_ht);
    }
}
