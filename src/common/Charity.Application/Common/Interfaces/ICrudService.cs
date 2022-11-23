using System.Linq.Expressions;
using Charity.Application.Common.Dto;
using Charity.Application.Models;
using Charity.Domain.Common;

namespace Charity.Application.Common.Interfaces;

public interface ICrudService<TEntity, TDto>
    where TEntity : BaseEntity
    where TDto : BaseDto
{
    Task<TDto?> GetOneAsync(Guid id);

    Task<PaginatedList<TDto>> GetPaginatedListAsync(IList<Expression<Func<TEntity, bool>>>? filters,
        ICollection<SortExpression>? sortExpressions, int pageIndex, int pageSize);

    Task<TDto> AddOneAsync(TDto model);
    Task<bool> UpdateOneAsync(Guid id, TDto model);
    Task<List<Guid>> UpdateManyAsync(List<Guid> ids, TDto model);
    Task<bool> DeleteOneAsync(Guid id);
    Task<List<Guid>> DeleteManyAsync(List<Guid> ids);
}