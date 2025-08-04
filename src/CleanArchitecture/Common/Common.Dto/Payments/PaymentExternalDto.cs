using Common.Enums;

namespace Common.Dto.Payments;

public class PaymentExternalDto
{
    public int Id { get; protected set; }
    public Guid Uuid { get; protected set; }
    public int OrderId { get; protected set; }
    public PaymentProvider Provider { get; protected set; }
    public PaymentStatus Status { get; protected set; }
    public decimal Value { get; protected set; }
    public string? ExternalId { get; protected set; }
    public string? UserPaymentCode { get; protected set; }

    public PaymentExternalDto(
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