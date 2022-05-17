using ECaterer.Contracts.Orders;
using ECaterer.Core.Models;
using ECaterer.WebApi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECaterer.WebApi.Common.Interfaces
{
    public interface IDietController
    {
        public Task<DietModel> GetDietByID(string id);
        public Task<IEnumerable<DietModel>> GetDiets(int? offset, int? limit, string sort, string name, string name_with, 
                                                bool? vegan, int? calories, int? colories_lt, int? colories_ht,
                                                int? price, int? price_lt, int? price_ht);
        public Task<Diet> AddDiet(CreateDietMealsModel dietInfo);
        public Task<Diet> EditDiet(string dietId, CreateDietMealsModel dietInfo);
        public Task<Diet> DeleteDiet(string dietId);
    }
}
