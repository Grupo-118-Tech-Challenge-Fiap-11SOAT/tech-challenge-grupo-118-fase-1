namespace Common.Dto.Payments;

// public class PaymentResponse(Payment payment)
// {
//     /// <summary>
//     /// The id of the payment
//     /// </summary>
//     public int Id { get; set; } = payment.Id;
//
//     /// <summary>
//     /// The uuid of the payment
//     /// </summary>
//     public Guid Uuid { get; set; } = payment.Uuid;
//
//     /// <summary>
//     /// The id of the order
//     /// </summary>
//     public int OrderId { get; set; } = payment.OrderId;
//
//     /// <summary>
//     /// The payment provider
//     /// </summary>
//     public PaymentProvider Provider { get; set; } = payment.Provider;
//
//     /// <summary>
//     /// The payment status
//     /// </summary>
//     public PaymentStatus Status { get; set; } = payment.Status;
//
//     /// <summary>
//     /// The code for user payment, depends on provider (ex: qr code)
//     /// </summary>
//     public string? UserPaymentCode { get; set; } = payment.UserPaymentCode;
//}

public class PaymentResponse
{
    /// <summary>
    /// The id of the payment
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The uuid of the payment
    /// </summary>
    public Guid Uuid { get; set; }

    /// <summary>
    /// The id of the order
    /// </summary>
    public int OrderId { get; set; }

    /// <summary>
    /// The payment provider
    /// </summary>
    public PaymentProvider Provider { get; set; }

    /// <summary>
    /// The payment status
    /// </summary>
    public PaymentStatus Status { get; set; }

    /// <summary>
    /// The code for user payment, depends on provider (ex: qr code)
    /// </summary>
    public string? UserPaymentCode { get; set; }
}