using Domain.Payments.Entities;
using Domain.Payments.Enumerators;

namespace Domain.Payments.Dtos;

public class PaymentResponse(Payment payment)
{
    /// <summary>
    /// The id of the payment
    /// </summary>
    public int Id { get; set; } = payment.Id;

    /// <summary>
    /// The uuid of the payment
    /// </summary>
    public Guid Uuid { get; set; } = payment.Uuid;

    /// <summary>
    /// The id of the order
    /// </summary>
    public int OrderId { get; set; } = payment.OrderId;

    /// <summary>
    /// The payment provider
    /// </summary>
    public PaymentProvider Provider { get; set; } = payment.Provider;

    /// <summary>
    /// The payment status
    /// </summary>
    public PaymentStatus Status { get; set; } = payment.Status;

    /// <summary>
    /// The code for user payment, depends on provider (ex: qr code)
    /// </summary>
    public string? UserPaymentCode { get; set; } = payment.UserPaymentCode;
}