using Charity.Application.Dto;
using Charity.Domain.Entities;

namespace Charity.Application.Interfaces;

public interface IDonatorService : ICrudService<Donator, DonatorDto>
{
}