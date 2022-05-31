using ECaterer.Core.Models;
using ECaterer.WebApi.Common.Interfaces;
using System;
using System.Linq;

namespace ECaterer.WebApi.Common.Filters.Meals
{
    public class MealSort : IFilter<Meal>
    {
        public string sort { get; private set; }

        public MealSort(string sort)
        {
            this.sort = sort;
        }

        public IQueryable<Meal> Filter(IQueryable<Meal> data)
        {
            IQueryable<Meal> result = null;
            switch (sort)
            {
                case "title(asc)":
                    result = data.OrderBy(m => m.Name);
                    break;
                case "title(desc)":
                    result = data.OrderByDescending(m => m.Name);
                    break;
                case "calories(asc)":
                    result = data.OrderBy(m => m.Calories);
                    break;
                case "calories(desc)":
                    result = data.OrderByDescending(m => m.Calories);
                    break;
                case null:
                    return data;
                default:
                    throw new Exception("Unexpected sort type");
            }
            return result;
        }
    }
}
