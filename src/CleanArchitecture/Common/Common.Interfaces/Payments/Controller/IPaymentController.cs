using Common.Dto.Payments;

namespace Common.Interfaces.Payments.Controller;

public interface IPaymentController
{
    Task<PaymentResponse> CreatePaymentAsync(PaymentRequest request, CancellationToken cancellationToken);

    Task<PaymentResponse> ConfirmPaymentAsync(int id, CancellationToken cancellationToken);

    Task ProcessCallbackAsync(PaymentCallbackRequest request, CancellationToken cancellationToken);
}