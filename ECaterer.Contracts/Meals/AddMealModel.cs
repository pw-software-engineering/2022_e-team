using System;
using System.Collections.Generic;
using System.Text;

namespace ECaterer.Contracts.Meals
{
    public class AddMealModel
    {
        public string Name { get; set; }
        public string[] AllergentList { get; set; }
        public string[] IngredientList { get; set; }
        public int Calories { get; set; }
        public bool Vegan { get; set; }
    }
}
