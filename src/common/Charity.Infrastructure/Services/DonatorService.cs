using Charity.Application.Common.Interfaces;
using Charity.Application.Dto;
using Charity.Application.Interfaces;
using Charity.Domain.Entities;
using Charity.Infrastructure.Common.Services;
using Charity.Infrastructure.Persistence;
using MapsterMapper;

namespace Charity.Infrastructure.Services;

public class DonatorService : BaseCrudService<Donator, DonatorDto>, IDonatorService
{
    public DonatorService(ApplicationDbContext dbContext, IFilterAdapter filterAdapter, ISortAdapter sortAdapter, IMapper mapper)
        : base(dbContext, filterAdapter, sortAdapter, mapper)
    {
    }
}