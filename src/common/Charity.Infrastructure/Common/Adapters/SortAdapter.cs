using System.Linq.Expressions;
using Charity.Application.Common.Interfaces;
using Charity.Application.Models;

namespace Charity.Infrastructure.Common.Adapters;

/*
 * Most of the code is written modifying the suggestion at this thread:
 * https://stackoverflow.com/questions/7265186/how-do-i-specify-the-linq-orderby-argument-dynamically
 */

public class SortAdapter : ISortAdapter
{
    public IQueryable<TEntity> ApplySortExpressions<TEntity>(IQueryable<TEntity> queryable,
        ICollection<SortExpression>? sortExpressions)
    {
        return sortExpressions == null || sortExpressions.Any() is false
            ? OrderByAsc(queryable, "Id")
            : ApplySortExpressionsInternal(queryable, sortExpressions);
    }

    private static IQueryable<TEntity> ApplySortExpressionsInternal<TEntity>(IQueryable<TEntity> queryable,
        ICollection<SortExpression> sortExpressions)
    {
        var firstExpression = sortExpressions.First();

        queryable = firstExpression.IsDescending
            ? OrderByDesc(queryable, firstExpression.Field)
            : OrderByAsc(queryable, firstExpression.Field);

        foreach (var expression in sortExpressions.Skip(1))
            queryable = expression.IsDescending
                ? ThenByDesc(queryable, expression.Field)
                : ThenByAsc(queryable, expression.Field);

        return queryable;
    }

    private static IQueryable<TEntity> OrderByAsc<TEntity>(IQueryable<TEntity> queryable, string? field) =>
        Order(queryable, field, "OrderBy");

    private static IQueryable<TEntity> ThenByAsc<TEntity>(IQueryable<TEntity> queryable, string? field) =>
        Order(queryable, field, "ThenBy");

    private static IQueryable<TEntity> OrderByDesc<TEntity>(IQueryable<TEntity> queryable, string? field) =>
        Order(queryable, field, "OrderByDescending");

    private static IQueryable<TEntity> ThenByDesc<TEntity>(IQueryable<TEntity> queryable, string? field) =>
        Order(queryable, field, "ThenByDescending");

    private static IQueryable<TEntity> Order<TEntity>(IQueryable<TEntity> queryable, string? field, string operation)
    {
        var type = typeof(TEntity);
        var objProperty = type.GetProperties()
            .First(p => p.Name.Equals(field, StringComparison.OrdinalIgnoreCase));
        var parameter = Expression.Parameter(type, "param");
        var propertyAccess = Expression.MakeMemberAccess(parameter, objProperty);
        var orderByExpression = Expression.Lambda(propertyAccess, parameter);
        var resultExpression = Expression.Call(typeof(Queryable), operation,
            new[] { type, objProperty.PropertyType },
            queryable.Expression, Expression.Quote(orderByExpression));
        return queryable.Provider.CreateQuery<TEntity>(resultExpression);
    }
}