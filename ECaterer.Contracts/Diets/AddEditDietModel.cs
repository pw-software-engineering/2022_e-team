using System;
using System.Collections.Generic;
using System.Text;

namespace ECaterer.Contracts.Diets
{
    public class AddEditDietModel
    {
        public string Name { get; set; }
        public string[] MealIds { get; set; }
        public int Price { get; set; }
    }
}
