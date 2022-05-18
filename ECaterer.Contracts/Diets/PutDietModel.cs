using System;
using System.Collections.Generic;
using System.Text;

namespace ECaterer.Contracts.Diets
{
    public class PutDietModel
    {
        public string Name { get; set; }
        public IEnumerable<string> MealIds { get; set; }
        public int Price { get; set; }
    }
}
