using ECaterer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECaterer.Web.DTO.MealsDTO
{
    public class MealDTO
    {
        public string Name { get; set; }
        public string[] AllergentList { get; set; }
        public string[] IngredientList { get; set; }
        public int Calories { get; set; }
        public bool Vegan { get; set; }
        public MealDTO() { }
        public MealDTO(Meal meal)
        {
            Name = meal.Name;
            Calories = meal.Calories;
            Vegan = meal.Vegan;
            AllergentList = meal.AllergentList.Select(al => al.Name).ToArray();
            IngredientList = meal.IngredientList.Select(ing => ing.Name).ToArray();
        }
    }
}
