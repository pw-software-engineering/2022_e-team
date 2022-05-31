using ECaterer.Core.Models;
using ECaterer.WebApi.Common.Interfaces;
using System.Linq;

namespace ECaterer.WebApi.Common.Filters.Meals
{
    public class MealNameWithFilter : IFilter<Meal>
    {
        public string name_with { get; private set; }

        public MealNameWithFilter(string name_with)
        {
            this.name_with = name_with;
        }

        public IQueryable<Meal> Filter(IQueryable<Meal> data)
        {
            if (name_with is null)
                return data;
            return data.Where(m => m.Name.Contains(name_with));
        }
    }
}
