using Domain;

namespace Infra.Database.SqlServer;

public abstract class BaseEntity
{
    public int Id { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset UpdatedAt { get; set; }

    public abstract BaseDomain ToDomain();

    public abstract void DomainToEntity(BaseDomain domain);
}