using ECaterer.Core.Models;
using ECaterer.WebApi.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECaterer.WebApi.Common.Filters.Diets
{
    public class DietPriceFilter : IFilter<Diet>
    {
        public int? price { get; private set; }

        public DietPriceFilter(int? price)
        {
            this.price = price;
        }

        public IQueryable<Diet> Filter(IQueryable<Diet> data)
        {
            if (price is null)
                return data;
            return data.Where(m => m.Price == price);
        }
    }
}
