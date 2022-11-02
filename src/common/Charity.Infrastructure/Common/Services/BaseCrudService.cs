using System.Linq.Expressions;
using Charity.Application.Common.Interfaces;
using Charity.Application.Models;
using Microsoft.EntityFrameworkCore;

namespace Charity.Infrastructure.Common.Services;

public abstract class BaseCrudService<TEntity, TDto> : ICrudService<TEntity, TDto>
    where TEntity : class
{
    private readonly DbSet<TEntity> _dbSet;
    
    protected BaseCrudService(DbSet<TEntity> dbSet) =>
        _dbSet = dbSet;
    
    public async Task<TDto> GetOneAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<PaginatedList<TDto>> GetPaginatedListAsync(Expression<Func<TEntity, bool>> filteringExpression, int pageIndex, int pageSize)
    {
        throw new NotImplementedException();
    }

    public async Task AddOneAsync(TDto model)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateOneAsync(Guid id, TDto model)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteOneAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}