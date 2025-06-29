using Common.Dto.Payments;

namespace TechChallengeFastFood.CleanArch.Infrastructure.Database.Payments.Entities;

public class Payment
{
    protected Payment()
    {
    }

    public int Id { get; protected set; }
    public Guid Uuid { get; protected set; } = Guid.NewGuid();
    public int OrderId { get; protected set; }
    public PaymentProvider Provider { get; protected set; }
    public PaymentStatus Status { get; protected set; }
    public decimal Value { get; protected set; }
    public string? ExternalId { get; protected set; }
    public string? UserPaymentCode { get; protected set; }
}