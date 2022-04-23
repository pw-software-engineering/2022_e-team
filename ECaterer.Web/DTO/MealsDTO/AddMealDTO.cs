using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECaterer.Web.DTO.MealsDTO
{
    public class AddMealDTO
    {
        public int MealId { get; set; }
        public string Name { get; set; }
        public string[] AllergenList { get; set; }
        public string[] IngredientList { get; set; }
        public int Calories { get; set; }
        public bool Vegan { get; set; }
    }
}
