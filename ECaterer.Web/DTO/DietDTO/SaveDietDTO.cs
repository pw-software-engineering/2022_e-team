using System.Collections.Generic;

namespace ECaterer.Web.DTO
{
    public class SaveDietDTO
    {
        public string Id { get; set; }
        public int Calories { get; set; }
        public string Description { get; set; }
        public bool Vegan { get; set; }

        public IEnumerable<MealsDTO.MealDTO> Meals { get; set; }
    }
}
