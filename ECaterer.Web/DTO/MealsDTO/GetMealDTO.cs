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
    }
}
