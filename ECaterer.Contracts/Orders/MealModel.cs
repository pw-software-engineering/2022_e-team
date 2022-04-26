﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ECaterer.Contracts.Orders
{
    public class MealModel
    {
        public string MealId { get; set; }

        public string Name { get; set; }

        public string[] IngredientList { get; set; }

        public string[] AllergentList { get; set; }

        public int Calories { get; set; }

        public bool Vegan { get; set; }
    }
}
