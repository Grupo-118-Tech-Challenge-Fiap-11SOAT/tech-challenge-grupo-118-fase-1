using Domain.Payments.Enumerators;

namespace Domain.Payments.Dtos;

public class PaymentRequest
{
    /// <summary>
    /// The id of the order
    /// </summary>
    public int OrderId { get; set; }

    /// <summary>
    /// The payment provider
    /// </summary>
    public PaymentProvider Provider { get; set; }
}