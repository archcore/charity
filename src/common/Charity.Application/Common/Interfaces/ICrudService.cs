using System.Linq.Expressions;
using Charity.Application.Models;

namespace Charity.Application.Common.Interfaces;

public interface ICrudService<TEntity, TDto>
{
    Task<TDto?> GetOneAsync(Guid id);

    Task<PaginatedList<TDto>> GetPaginatedListAsync(Expression<Func<TEntity, bool>> filterExpression,
        ICollection<SortExpression> sortExpressions, int pageIndex, int pageSize);

    Task AddOneAsync(TDto model);
    Task<bool> UpdateOneAsync(Guid id, TDto model);
    Task<bool> DeleteOneAsync(Guid id);
}