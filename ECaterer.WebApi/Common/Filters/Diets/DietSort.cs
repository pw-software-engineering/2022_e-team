using ECaterer.Core.Models;
using ECaterer.WebApi.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECaterer.WebApi.Common.Filters.Diets
{
    public class DietSort : IFilter<Diet>
    {
        public string sort { get; private set; }

        public DietSort(string sort)
        {
            this.sort = sort;
        }

        public IQueryable<Diet> Filter(IQueryable<Diet> data)
        {
            IQueryable<Diet> result = null;
            switch (sort)
            {
                case "title(asc)":
                    result = data.OrderBy(d => d.Title);
                    break;
                case "title(desc)":
                    result = data.OrderByDescending(d => d.Title);
                    break;
                case "calories(asc)":
                    result = data.OrderBy(d => d.Calories);
                    break;
                case "calories(desc)":
                    result = data.OrderByDescending(d => d.Calories);
                    break;
                case "price(asc)":
                    result = data.OrderBy(d => d.Price);
                    break;
                case "price(desc)":
                    result = data.OrderByDescending(d => d.Price);
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
