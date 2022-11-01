using System.Linq.Expressions;
using Charity.Application.Models;

namespace Charity.Application.Interfaces;

public interface ICrudService<TEntity, TDto>
{
    Task<TDto> GetOneAsync(Guid id);
    Task<PaginatedList<TDto>> GetPaginatedListAsync(Expression<Func<TEntity, bool>> filteringExpression, int pageIndex, int pageSize);
    Task AddOneAsync(TDto model);
    Task UpdateOneAsync(Guid id, TDto model);
    Task DeleteOneAsync(Guid id);
}