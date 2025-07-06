using Common.Enums;

namespace Common.Dto.Payments.Database;

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

    public Payment(
        int id,
        Guid uuid,
        int orderId,
        PaymentProvider provider,
        PaymentStatus status,
        decimal value = 0,
        string? externalId = null,
        string? userPaymentCode = null)
    {
        Id = id;
        Uuid = uuid;
        OrderId = orderId;
        Provider = provider;
        Status = status;
        Value = value;
        ExternalId = externalId;
        UserPaymentCode = userPaymentCode;
    }
}