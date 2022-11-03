using System.Linq.Expressions;
using Charity.Application.Common.Interfaces;

namespace Charity.Infrastructure.Common.Adapters;

public class FilterAdapter : IFilterAdapter
{
    public IQueryable<TEntity> ApplyFilterExpressions<TEntity>(IQueryable<TEntity> queryable,
        IList<Expression<Func<TEntity, bool>>>? filters)
    {
        if (filters == null || filters.Count == 0)
            return queryable;

        foreach (var filter in filters)
            queryable = queryable.Where(filter);

        return queryable;
    }
}