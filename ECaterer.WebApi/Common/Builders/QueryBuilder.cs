using ECaterer.WebApi.Common.Interfaces;
using System.Linq;

namespace ECaterer.WebApi.Common.Builders
{
    public class QueryBuilder<T>
    {
        private IQueryable<T> items;

        public QueryBuilder(IQueryable<T> items)
        {
            this.items = items;
        }

        public QueryBuilder<T> PerformAction(IFilter<T> filter)
        {
            return new QueryBuilder<T>(filter.Filter(items));
        }

        public IQueryable<T> GetQuery()
        {
            return items;
        }
    }
}
