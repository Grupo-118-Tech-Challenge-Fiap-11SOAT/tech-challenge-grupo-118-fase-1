using Domain.Base.Exceptions;
using Domain.Payments.Enumerators;

namespace Domain.Payments.Entities;

public class Payment : BaseDomain
{
    public int Id { get; protected set; }
    public Guid Uuid { get; protected set; } = Guid.NewGuid();
    public int OrderId { get; protected set; }
    public PaymentProvider Provider { get; protected set; }
    public PaymentStatus Status { get; protected set; }
    public decimal Value { get; protected set; }
    public string? ExternalId { get; protected set; }
    public string? UserPaymentCode { get; protected set; }

    public Payment(int orderId,
        PaymentProvider provider,
        decimal value)
    {
        this.OrderId = orderId;
        this.Provider = provider;
        SetValue(value);
        SetStatus(PaymentStatus.Pending);
    }

    public void SetStatus(PaymentStatus status)
    {
        this.Status = status;
    }

    public void SetExternalId(string externalId)
    {
        this.ExternalId = externalId;
    }

    public void SetUserPaymentCode(string? userPaymentCode)
    {
        this.UserPaymentCode = userPaymentCode;
    }

    protected void SetValue(decimal value)
    {
        if (value <= 0)
            throw new InvalidAtributeException(nameof(value));

        this.Value = value;
    }
}