using System.Linq.Expressions;
using Charity.Application.Common.Dto;
using Charity.Application.Common.Interfaces;
using Charity.Application.Models;
using Charity.Domain.Common;
using Charity.Infrastructure.Persistence;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace Charity.Infrastructure.Common.Services;

public abstract class BaseCrudService<TEntity, TDto> : ICrudService<TEntity, TDto>
    where TEntity : BaseEntity
    where TDto : BaseDto
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IFilterAdapter _filterAdapter;
    private readonly ISortAdapter _sortAdapter;
    private readonly IMapper _mapper;

    protected BaseCrudService(ApplicationDbContext dbContext, IFilterAdapter filterAdapter, ISortAdapter sortAdapter, IMapper mapper)
    {
        _dbContext = dbContext;
        _filterAdapter = filterAdapter;
        _sortAdapter = sortAdapter;
        _mapper = mapper;
    }

    public async Task<TDto?> GetOneAsync(Guid id)
    {
        var entity = await _dbContext.Set<TEntity>().FindAsync(id);
        return entity == null ? default : _mapper.Map<TDto>(entity);
    }

    public async Task<PaginatedList<TDto>> GetPaginatedListAsync(IList<Expression<Func<TEntity, bool>>>? filters,
        ICollection<SortExpression>? sortExpressions, int pageIndex, int pageSize)
    {
        var queryable = _dbContext.Set<TEntity>()
            .AsNoTracking();
        
        queryable = _filterAdapter.ApplyFilterExpressions(queryable, filters);
        var count = await queryable.CountAsync();
        queryable = _sortAdapter.ApplySortExpressions(queryable, sortExpressions);
        queryable = queryable.Skip((pageIndex - 1) * pageSize).Take(pageSize);

        var items = queryable.Select(entity => _mapper.Map<TDto>(entity)).ToList();

        return new PaginatedList<TDto>(items, count, pageIndex, pageSize);
    }

    public Task<TDto> AddOneAsync(TDto model)
    {
        if (model == null)
            throw new ArgumentNullException(nameof(model));

        return AddOneInternalAsync(model);
    }

    private async Task<TDto> AddOneInternalAsync(TDto model)
    {
        var entity = _mapper.Map<TEntity>(model);

        await _dbContext.Set<TEntity>().AddAsync(entity);
        await _dbContext.SaveChangesAsync();

        model.Id = entity.Id;
        return model;
    }

    public Task<bool> UpdateOneAsync(Guid id, TDto model)
    {
        if (model == null)
            throw new ArgumentNullException(nameof(model));

        return UpdateOneInternalAsync(id, model);
    }

    private async Task<bool> UpdateOneInternalAsync(Guid id, TDto model)
    {
        var entity = await _dbContext.Set<TEntity>().FindAsync(id);
        if (entity == null)
            return false;

        _dbContext.Entry(entity).State = EntityState.Detached;
        entity = _mapper.Map<TEntity>(model);
        entity.Id = id;

        _dbContext.Set<TEntity>().Update(entity);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public Task<List<Guid>> UpdateManyAsync(List<Guid> ids, TDto model)
    {
        if (model == null)
            throw new ArgumentNullException(nameof(model));

        return UpdateManyInternalAsync(ids, model);
    }

    private async Task<List<Guid>> UpdateManyInternalAsync(List<Guid> ids, TDto model)
    {
        ids = await _dbContext.Set<TEntity>()
            .Where(m => ids.Contains(m.Id))
            .AsNoTracking()
            .Select(m => m.Id)
            .ToListAsync();

        var entities = ids.Select(id => {
            var entity = _mapper.Map<TEntity>(model);
            entity.Id = id;
            return entity;
        }).ToList();

        _dbContext.Set<TEntity>().UpdateRange(entities);
        await _dbContext.SaveChangesAsync();
        return ids;
    }

    public async Task<bool> DeleteOneAsync(Guid id)
    {
        var entity = await _dbContext.Set<TEntity>().FindAsync(id);
        if (entity == null)
            return false;
        
        _dbContext.Remove(entity);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<List<Guid>> DeleteManyAsync(List<Guid> ids)
    {
        var entities = await _dbContext.Set<TEntity>()
            .Where(m => ids.Contains(m.Id))
            .ToListAsync();
        
        _dbContext.RemoveRange(entities);
        await _dbContext.SaveChangesAsync();
        return entities.Select(m => m.Id).ToList();
    }
}