using ECaterer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECaterer.Web.DTO.DealDTO
{
    public class DietDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int Calories { get; set; }
        public Meal[] Meals { get; set; }
        public bool Vegan { get; set; }

        public DietDTO(Diet diet)
        {
            Title = diet.Title;
            Description = diet.Description;
            Calories = diet.Calories;
            Meals = diet.Meals.ToArray();
            Vegan = diet.Vegan;
        }
    }
}
