using NodaTime;

namespace Charity.Domain.Common;

public abstract class BaseEntity
{
    public Guid Id { get; set; }
    public Instant CreatedAt { get; set; }
    public Instant UpdatedAt { get; set; }
}