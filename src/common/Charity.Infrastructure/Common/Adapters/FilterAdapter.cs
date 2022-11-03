using System.Linq.Expressions;
using Charity.Application.Common.Interfaces;

namespace Charity.Infrastructure.Common.Adapters;

public class FilterAdapter : IFilterAdapter
{
    public IQueryable<TEntity> ApplyFilterExpressions<TEntity>(IQueryable<TEntity> queryable,
        IList<Expression<Func<TEntity, bool>>>? filters)
    {
        return filters == null || filters.Count == 0
            ? queryable
            : ApplyFilterExpressionsInternal(queryable, filters);
    }

    private static IQueryable<TEntity> ApplyFilterExpressionsInternal<TEntity>(IQueryable<TEntity> queryable,
        IList<Expression<Func<TEntity, bool>>>? filters)
    {
        var parameter = Expression.Parameter(typeof(TEntity), "obj");

        var firstFilter = filters[0];
        var filterExpression = Expression.Lambda<Func<TEntity, bool>>(firstFilter, parameter);

        if (filters.Count > 1)
        {
            Expression outerExpression = filters[0];
            for (var i = 1; i < filters.Count; ++i)
            {
                var aux = outerExpression;
                outerExpression = Expression.And(aux, filters[i]);
            }

            filterExpression = Expression.Lambda<Func<TEntity, bool>>(outerExpression, parameter);
        }

        return queryable.Where(filterExpression);
    }
}