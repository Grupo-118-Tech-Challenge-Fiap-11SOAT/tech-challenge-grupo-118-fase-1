namespace Common.Dto.Payments;

public class PaymentExternalDto
{
    public int Id { get; protected set; }
    public Guid Uuid { get; protected set; } = Guid.NewGuid();
    public int OrderId { get; protected set; }
    public PaymentProvider Provider { get; protected set; }
    public PaymentStatus Status { get; protected set; }
    public decimal Value { get; protected set; }
    public string? ExternalId { get; protected set; }
    public string? UserPaymentCode { get; protected set; }
}