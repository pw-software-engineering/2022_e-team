using ECaterer.Core.Models;
using ECaterer.WebApi.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECaterer.WebApi.Common.Filters.Diets
{
    public class DietVeganFilter : IFilter<Diet>
    {
        public bool? vegan { get; private set; }

        public DietVeganFilter(bool? vegan)
        {
            this.vegan = vegan;
        }

        public IQueryable<Diet> Filter(IQueryable<Diet> data)
        {
            if (vegan is null)
                return data;
            return data.Where(d => d.Vegan == vegan);
        }
    }
}
