using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECaterer.WebApi.Common.Queries
{
    public class DietQuery
    {
        public int? Offset { get; set; }
        public int? Limit { get; set; }
        public string Sort { get; set; }
        public string Name { get; set; }
        public string NameWith { get; set; }
        public bool? Vegan { get; set; }
        public int? Calories { get; set; }
        public int? CaloriesLt { get; set; }
        public int? CaloriesHt { get; set; }
        public int? Price { get; set; }
        public int? PriceLt { get; set; }
        public int? PriceHt { get; set; }
    }
}
