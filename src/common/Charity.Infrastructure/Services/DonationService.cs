using Charity.Application.Dto;
using Charity.Application.Interfaces;
using Charity.Domain.Entities;
using Charity.Infrastructure.Common.Services;
using Charity.Infrastructure.Persistence;

namespace Charity.Infrastructure.Services;

public class DonationService : BaseCrudService<Donation, DonationDto>, IDonationService
{
    public DonationService(ApplicationDbContext dbContext) : base(dbContext.Donations)
    {
    }
}