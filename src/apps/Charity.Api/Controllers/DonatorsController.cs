using System.Linq.Expressions;
using Charity.Api.Common.Controllers;
using Charity.Api.Requests;
using Charity.Application.Dto;
using Charity.Application.Interfaces;
using Charity.Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Charity.Api.Controllers;

[ApiController]
[Route("v1/[controller]")]
public class DonatorsController : BaseCrudController<Donator, DonatorDto, IDonatorService, DonatorPaginatedListRequest>
{
    public DonatorsController(IDonatorService service, IValidator<DonatorDto> validator)
        : base(service, validator)
    {
    }

    protected override IEnumerable<Expression<Func<Donator, bool>>>? GetPaginatedListFilters(DonatorPaginatedListRequest request)
    {
        if (!string.IsNullOrEmpty(request.Name))
            yield return d => EF.Functions.Like(d.Name!, request.Name);
        
        if (!string.IsNullOrEmpty(request.Document))
            yield return d => EF.Functions.Like(d.Document!, request.Document);
        
        if (request.DateOfBirthMin.HasValue)
            yield return d => d.DateOfBirth >= request.DateOfBirthMin.Value;
        
        if (request.DateOfBirthMax.HasValue)
            yield return d => d.DateOfBirth <= request.DateOfBirthMax.Value;
        
        if (!string.IsNullOrEmpty(request.Occupation))
            yield return d => EF.Functions.Like(d.Occupation ?? string.Empty, request.Occupation);
        
        if (request.Type.HasValue)
            yield return d => d.Type == request.Type.Value;
    }
}