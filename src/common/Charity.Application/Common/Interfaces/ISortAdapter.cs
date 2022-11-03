using Charity.Application.Models;

namespace Charity.Application.Common.Interfaces;

public interface ISortAdapter
{
    IQueryable<TEntity> ApplySortExpressions<TEntity>(IQueryable<TEntity> queryable, ICollection<SortExpression>? sortExpressions);
}