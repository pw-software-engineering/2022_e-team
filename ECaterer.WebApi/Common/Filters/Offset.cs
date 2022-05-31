using ECaterer.Core.Models.Enums;
using ECaterer.WebApi.Common.Interfaces;
using NodaTime;
using System;
using System.Linq;

namespace ECaterer.WebApi.Common.Filters
{
    public class Offset<T> : IFilter<T>
    {
        public int? offset { get; private set; }

        public Offset(int? offset)
        {
            this.offset = offset;
        }

        public IQueryable<T> Filter(IQueryable<T> data)
        {
            if (offset is null)
                return data;
            return data.Skip((int)offset);
        }
    }
}
