using Common.Enums;
using TechChallengeFastFood.CleanArch.Domain.Entities.Base.Exceptions;

namespace TechChallengeFastFood.CleanArch.Domain.Entities.Payments.Entities;

public class Payment
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
        decimal value,
        int id = 0,
        string? externalId = null,
        string? userPaymentCode = null)
    {
        if (id != 0)
            this.Id = id;
            
        if(externalId != null)
            this.ExternalId = externalId;
        
        if(userPaymentCode != null)
            this.UserPaymentCode = userPaymentCode;
        
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

    public void SetCreatedId(int id)
    {
        if (id <= 0)
            throw new InvalidAtributeException(nameof(id));

        this.Id = id;
    }

    protected void SetValue(decimal value)
    {
        if (value <= 0)
            throw new InvalidAtributeException(nameof(value));

        this.Value = value;
    }

    public void IsPaymentOnPendingStatus()
    {
        if (this.Status != PaymentStatus.Pending)
            throw new DomainException($"Payment with id {this.Id} is not in a pending state.");
    }

    public void SetStatusToApproved()
    {
        SetStatus(PaymentStatus.Approved);
    }
}