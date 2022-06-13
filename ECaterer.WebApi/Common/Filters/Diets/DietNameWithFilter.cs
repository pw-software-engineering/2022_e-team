using ECaterer.Core.Models;
using ECaterer.WebApi.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECaterer.WebApi.Common.Filters.Diets
{
    public class DietNameWithFilter : IFilter<Diet>
    {
        public string name_with { get; private set; }

        public DietNameWithFilter(string name_with)
        {
            this.name_with = name_with;
        }

        public IQueryable<Diet> Filter(IQueryable<Diet> data)
        {
            if (name_with is null)
                return data;
            return data.Where(d => d.Title.Contains(name_with));
        }
    }
}
