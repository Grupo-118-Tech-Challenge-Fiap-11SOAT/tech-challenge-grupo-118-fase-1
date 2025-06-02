
using Domain.Payments.Enumerators;

namespace Domain.Payments.Dtos;

public class ProcessedPaymentDto
{
    /// <summary>
    /// The payment code in provider esternal system
    /// </summary>
    public string ExternalId { get; set; }

    /// <summary>
    /// The payment status
    /// </summary>
    public PaymentStatus Status { get; set; }

    /// <summary>
    /// The code for user payment, depends on provider (ex: qr code)
    /// </summary>
    public string? UserPaymentCode { get; set; }
}