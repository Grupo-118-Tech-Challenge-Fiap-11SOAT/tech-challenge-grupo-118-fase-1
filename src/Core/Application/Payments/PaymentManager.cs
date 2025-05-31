using Domain.Order.Services.Interfaces;
using Domain.Payments.Dtos;
using Domain.Payments.Entities;
using Domain.Payments.Ports.In;
using Domain.Payments.Ports.Out;
using Domain.Payments.Services.Interfaces;

namespace Application.Payments;

public class PaymentManager(IPaymentProcessorFactory factory, IPaymentRepository repository,
    IPaymentService paymentService, IOrderService orderService) : IPaymentManager
{
    public async Task<PaymentResponse> CreatePaymentAsync(PaymentRequest request, CancellationToken cancellationToken = default)
    {
        Domain.Order.Entities.Order order = await orderService.ValidadeByIdAsync(request.OrderId, cancellationToken);

        var payment = new Payment(request.OrderId, request.Provider, order.Total);

        IPaymentProcessor processor = factory.GetProcessor(payment.Provider);
        ProcessedPaymentDto paymentData = await processor.ProcessAsync(payment, cancellationToken);
        UpdatePaymentData(payment, paymentData);

        await repository.CreateAsync(payment, cancellationToken);

        return new PaymentResponse(payment);
    }

    public async Task<PaymentResponse> ConfirmPaymentAsync(int id, CancellationToken cancellationToken = default)
    {
        Payment payment = await paymentService.ValidateByIdAsync(id, cancellationToken);
        Domain.Order.Entities.Order order = await orderService.ValidadeByIdAsync(payment.OrderId, cancellationToken);
        await paymentService.ConfirmAsync(order, payment, cancellationToken);
        return new PaymentResponse(payment);
    }

    public async Task<PaymentCallbackResponse> ProcessCallbackAsync(PaymentCallbackRequest request, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException("This method is not implemented yet.");
    }
 
    private void UpdatePaymentData(Payment payment, ProcessedPaymentDto paymentData)
    {
        payment.SetExternalId(paymentData.ExternalId);
        payment.SetUserPaymentCode(paymentData.UserPaymentCode);
        payment.SetStatus(paymentData.Status);
    }
}