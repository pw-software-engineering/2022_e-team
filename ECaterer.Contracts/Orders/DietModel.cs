using System;
using System.Collections.Generic;
using System.Text;

namespace ECaterer.Contracts.Orders
{
    public class DietModel
    {
        public string DietId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public MealModel Meals { get; set; }

        public int Price { get; set; }

        public bool Vegan { get; set; }
    }
}
