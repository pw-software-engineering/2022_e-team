using ECaterer.Web.DTO.MealsDTO;
using System.Collections.Generic;

namespace ECaterer.Web.DTO
{
    public class SaveDietDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Calories { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public bool Vegan { get; set; }
        public IEnumerable<MealDTO> Meals { get; set; }
    }
}
