using ECaterer.Core.Models.Enums;
using ECaterer.WebApi.Common.Interfaces;
using NodaTime;
using System;
using System.Linq;

namespace ECaterer.WebApi.Common.Filters
{
    public class Limit<T> : IFilter<T>
    {
        public int? limit { get; private set; }

        public Limit(int? limit)
        {
            this.limit = limit;
        }

        public IQueryable<T> Filter(IQueryable<T> data)
        {
            if (limit is null)
                return data;
            return data.Take((int)limit);
        }
    }
}
