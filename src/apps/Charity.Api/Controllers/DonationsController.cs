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
public class DonationsController : BaseCrudController<Donation, DonationDto, IDonationService, DonationPaginatedListRequest>
{
    public DonationsController(IDonationService service, IValidator<DonationDto> validator)
        : base(service, validator)
    {
    }

    protected override IEnumerable<Expression<Func<Donation, bool>>>? GetPaginatedListFilters(DonationPaginatedListRequest request)
    {
        if (request.OrganizationId.HasValue)
            yield return d => d.OrganizationId == request.OrganizationId.Value;
        
        if (request.DonatorId.HasValue)
            yield return d => d.DonatorId == request.DonatorId.Value;
        
        if (request.ValueMin.HasValue)
            yield return d => d.Value >= request.ValueMin.Value;
        
        if (request.ValueMax.HasValue)
            yield return d => d.Value <= request.ValueMax.Value;
        
        if (request.DonatedAtMin.HasValue)
            yield return d => d.DonatedAt >= request.DonatedAtMin.Value;
        
        if (request.DonatedAtMax.HasValue)
            yield return d => d.DonatedAt <= request.DonatedAtMax.Value;
        
        if (!string.IsNullOrEmpty(request.Description))
            yield return d => EF.Functions.Like(d.Description!, request.Description);
    }
}