using Charity.Application.Common.Interfaces;
using Charity.Application.Dto;
using Charity.Domain.Entities;

namespace Charity.Application.Interfaces;

public interface IOrganizationService : ICrudService<Organization, OrganizationDto>
{
}