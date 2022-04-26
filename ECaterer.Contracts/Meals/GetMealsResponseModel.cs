using System;
using System.Collections.Generic;
using System.Text;

namespace ECaterer.Contracts.Meals
{
    public class GetMealsResponseModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Calories { get; set; }
        public bool Vegan { get; set; }
    }
}
