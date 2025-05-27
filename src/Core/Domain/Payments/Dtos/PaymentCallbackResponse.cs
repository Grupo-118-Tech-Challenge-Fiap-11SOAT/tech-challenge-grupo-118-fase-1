using Domain.Payments.Enumerators;

namespace Domain.Payments.Dtos;

public class PaymentCallbackResponse
{
    public PaymentStatus Status { get; set; }
}