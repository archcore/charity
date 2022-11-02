using Charity.Application.Dto;
using Charity.Application.Interfaces;
using Charity.Domain.Entities;
using Charity.Infrastructure.Common.Services;
using Charity.Infrastructure.Persistence;

namespace Charity.Infrastructure.Services;

public class DonatorService : BaseCrudService<Donator, DonatorDto>, IDonatorService
{
    public DonatorService(ApplicationDbContext dbContext) : base(dbContext.Donators)
    {
    }
}