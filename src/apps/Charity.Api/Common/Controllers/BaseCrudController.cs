using System.Linq.Expressions;
using Charity.Api.Requests;
using Charity.Application.Common.Dto;
using Charity.Application.Common.Interfaces;
using Charity.Application.Models;
using Charity.Domain.Common;
using Microsoft.AspNetCore.Mvc;

namespace Charity.Api.Common.Controllers;

public abstract class BaseCrudController<TEntity, TDto, TService, TPaginatedListRequest> : ControllerBase
    where TEntity : BaseEntity
    where TDto : BaseDto
    where TService : ICrudService<TEntity, TDto>
    where TPaginatedListRequest : PaginatedListRequest
{
    protected BaseCrudController(TService service) =>
        Service = service;

    protected TService Service { get; }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
    {
        var model = await Service.GetOneAsync(id);
        return model == null
            ? NotFound()
            : Ok(model);
    }

    public async Task<IActionResult> GetPaginatedListAsync([FromQuery] TPaginatedListRequest request)
    {
        var filters = GetPaginatedListFilters(request);
        
        var payload = await Service.GetPaginatedListAsync(
            filters?.ToList(),
            BuildSortExpressions(request),
            request.PageIndex,
            request.PageSize);

        return Ok(payload);
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] TDto model)
    {
        model = await Service.AddOneAsync(model);
        // ReSharper disable once Mvc.ActionNotResolved
        return CreatedAtAction(nameof(GetByIdAsync), new { id = model.Id }, model);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> PutAsync([FromRoute] Guid id, [FromBody] TDto model)
    {
        var isUpdated = await Service.UpdateOneAsync(id, model);
        return isUpdated
            ? Ok(model)
            : NotFound();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
    {
        var isUpdated = await Service.DeleteOneAsync(id);
        return isUpdated
            ? Ok()
            : NotFound();
    }

    protected abstract IEnumerable<Expression<Func<TEntity, bool>>>? GetPaginatedListFilters(
        TPaginatedListRequest request);

    private static ICollection<SortExpression>? BuildSortExpressions(TPaginatedListRequest request) =>
        request.Sort?.Select(entry => new SortExpression(
            entry.Key,
            entry.Value.Equals("desc", StringComparison.OrdinalIgnoreCase))
        ).ToList();
}