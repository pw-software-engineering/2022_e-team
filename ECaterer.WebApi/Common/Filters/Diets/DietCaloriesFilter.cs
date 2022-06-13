using ECaterer.Core.Models;
using ECaterer.WebApi.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECaterer.WebApi.Common.Filters.Diets
{
    public class DietCaloriesFilter : IFilter<Diet>
    {
        public int? calories { get; private set; }

        public DietCaloriesFilter(int? calories)
        {
            this.calories = calories;
        }

        public IQueryable<Diet> Filter(IQueryable<Diet> data)
        {
            if (calories is null)
                return data;
            return data.Where(m => m.Calories == calories);
        }
    }
}
