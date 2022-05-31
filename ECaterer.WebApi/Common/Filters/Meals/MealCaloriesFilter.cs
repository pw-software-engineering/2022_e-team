using ECaterer.Core.Models;
using ECaterer.WebApi.Common.Interfaces;
using System.Linq;

namespace ECaterer.WebApi.Common.Filters.Meals
{
    public class MealCaloriesFilter : IFilter<Meal>
    {
        public int? calories { get; private set; }

        public MealCaloriesFilter(int? calories)
        {
            this.calories = calories;
        }

        public IQueryable<Meal> Filter(IQueryable<Meal> data)
        {
            if (calories is null)
                return data;
            return data.Where(m => m.Calories == calories);
        }
    }
}
