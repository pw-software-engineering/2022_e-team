using ECaterer.Contracts.Diets;
using ECaterer.Core.Models;
using ECaterer.WebApi.Common.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECaterer.WebApi.Common.Interfaces
{
    public interface IDietRepository
    {
        Task<Diet> GetDietById(string dietId);
        Task<IEnumerable<Diet>> GetDiets(GetDietsQueryModel query);
        Task<Diet> AddDiet(DietModel dietModel);
        Task<Diet> EditDiet(string dietId, DietModel dietModel);
        Task<(bool exists, bool deleted)> DeleteDiet(string dietId);
    }
}
