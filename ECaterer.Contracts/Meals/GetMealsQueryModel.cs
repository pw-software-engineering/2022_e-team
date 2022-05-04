using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECaterer.Contracts.Meals
{
    public class GetMealsQueryModel
    {
        public int? Offset { get; set; }
        public int? Limit { get; set; }
        public string Sort { get; set; }
        public string Name { get; set; }
        public string Name_with { get; set; }
        public bool? Vegan { get; set; }
        public int? Calories { get; set; }
        public int? Calories_lt { get; set; }
        public int? Calories_ht { get; set; }
    }
}
