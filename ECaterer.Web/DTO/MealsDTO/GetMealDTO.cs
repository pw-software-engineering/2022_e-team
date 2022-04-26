using ECaterer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECaterer.Web.DTO.MealsDTO
{
    public class GetMealDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Calories { get; set; }
        public bool Vegan { get; set; }
        public GetMealDTO() { }
        public GetMealDTO(Meal meal) 
        {
            Id = meal.MealId;
            Name = meal.Name;
            Calories = meal.Calories;
            Vegan = meal.Vegan;
        }
    }
}
