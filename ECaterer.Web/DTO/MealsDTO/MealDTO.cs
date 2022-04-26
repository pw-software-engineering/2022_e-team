using Microsoft.AspNetCore.Mvc;
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
    } 
}
