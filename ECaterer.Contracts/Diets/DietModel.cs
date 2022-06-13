using ECaterer.Contracts.Meals;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECaterer.Contracts.Diets
{
    public class DietModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public MealModel[] Meals { get; set; }
        public bool Vegan { get; set; }
    }
}
