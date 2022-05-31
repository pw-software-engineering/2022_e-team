using ECaterer.Core.Models;
using ECaterer.WebApi.Common.Interfaces;
using System.Linq;

namespace ECaterer.WebApi.Common.Filters.Meals
{
    public class MealVeganFilter : IFilter<Meal>
    {
        public bool? vegan { get; private set; }

        public MealVeganFilter(bool? vegan)
        {
            this.vegan = vegan;
        }

        public IQueryable<Meal> Filter(IQueryable<Meal> data)
        {
            if (vegan is null)
                return data;
            return data.Where(m => m.Vegan == vegan);
        }
    }
}
