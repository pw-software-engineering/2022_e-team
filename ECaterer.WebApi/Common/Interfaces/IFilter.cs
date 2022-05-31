using ECaterer.Core.Models;
using System.Linq;

namespace ECaterer.WebApi.Common.Interfaces
{
    public interface IFilter<T>
    {
        IQueryable<T> Filter(IQueryable<T> data);
    }
}
