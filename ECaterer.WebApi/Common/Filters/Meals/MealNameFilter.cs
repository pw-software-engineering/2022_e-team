using ECaterer.Core.Models;
using ECaterer.WebApi.Common.Interfaces;
using System.Linq;

namespace ECaterer.WebApi.Common.Filters.Meals
{
    public class MealNameFilter : IFilter<Meal>
    {
        public string name { get; private set; }

        public MealNameFilter(string name)
        {
            this.name = name;
        }

        public IQueryable<Meal> Filter(IQueryable<Meal> data)
        {
            if (name is null)
                return data;
            return data.Where(m => m.Name == name);
        }
    }
}
