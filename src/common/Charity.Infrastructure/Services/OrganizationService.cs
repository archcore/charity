using Charity.Application.Dto;
using Charity.Application.Interfaces;
using Charity.Domain.Entities;
using Charity.Infrastructure.Common.Services;
using Charity.Infrastructure.Persistence;

namespace Charity.Infrastructure.Services;

public class OrganizationService : BaseCrudService<Organization, OrganizationDto>, IOrganizationService
{
    public OrganizationService(ApplicationDbContext dbContext) : base(dbContext.Organizations)
    {
    }
}