using System.Linq.Expressions;

namespace Charity.Application.Common.Interfaces;

public interface IFilterAdapter
{
    IQueryable<TEntity> ApplyFilterExpressions<TEntity>(IQueryable<TEntity> queryable,
        IList<Expression<Func<TEntity, bool>>>? filters);
}