using ECaterer.Core.Models;
using ECaterer.WebApi.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECaterer.WebApi.Common.Filters.Diets
{
    public class DietNameFilter : IFilter<Diet>
    {
        public string name { get; private set; }

        public DietNameFilter(string name)
        {
            this.name = name;
        }

        public IQueryable<Diet> Filter(IQueryable<Diet> data)
        {
            if (name is null)
                return data;
            return data.Where(m => m.Title == name);
        }
    }
}
