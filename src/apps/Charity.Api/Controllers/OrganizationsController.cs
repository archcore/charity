using System.Linq.Expressions;
using Charity.Api.Common.Controllers;
using Charity.Api.Requests;
using Charity.Application.Dto;
using Charity.Application.Interfaces;
using Charity.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Charity.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class OrganizationsController : BaseCrudController<Organization, OrganizationDto, IOrganizationService, OrganizationPaginatedListRequest>
{
    public OrganizationsController(IOrganizationService service) : base(service)
    {
    }

    protected override IEnumerable<Expression<Func<Organization, bool>>>? GetPaginatedListFilters(OrganizationPaginatedListRequest request)
    {
        if (!string.IsNullOrEmpty(request.LegalName))
            yield return d => EF.Functions.Like(d.LegalName, request.LegalName);
        
        if (!string.IsNullOrEmpty(request.FriendlyName))
            yield return d => EF.Functions.Like(d.FriendlyName, request.FriendlyName);
        
        if (!string.IsNullOrEmpty(request.Cause))
            yield return d => EF.Functions.Like(d.Cause, request.Cause);
        
        if (!string.IsNullOrEmpty(request.Country))
            yield return d => d.Country.Equals(request.Country, StringComparison.OrdinalIgnoreCase);
        
        if (request.FoundationDateMin.HasValue)
            yield return d => d.FoundationDate >= request.FoundationDateMin.Value;
        
        if (request.FoundationDateMax.HasValue)
            yield return d => d.FoundationDate <= request.FoundationDateMax.Value;

        if (request.IsAcceptingDonations.HasValue)
            yield return d => d.IsAcceptingDonations == request.IsAcceptingDonations.Value;
    }
}