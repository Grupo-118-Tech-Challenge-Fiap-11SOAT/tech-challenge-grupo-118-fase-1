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
    private const string PAYMENT_CREATED = "payment.created";

    public async Task<PaymentResponse> CreatePaymentAsync(PaymentRequest request, CancellationToken cancellationToken = default)
    {
        Domain.Order.Entities.Order order = await orderService.ValidateByIdAsync(request.OrderId, cancellationToken);

        var payment = new Payment(request.OrderId, request.Provider, order.Total);

        IPaymentProcessor processor = factory.GetProcessor(payment.Provider);
        ProcessedPaymentDto paymentData = await processor.ProcessAsync(payment, order, cancellationToken);
        UpdatePaymentData(payment, paymentData);

        await repository.CreateAsync(payment, cancellationToken);

        return new PaymentResponse(payment);
    }

    public async Task ProcessCallbackAsync(Guid paymentUuid, MercadoPagoCallbackRequest request, CancellationToken cancellationToken = default)
    {
        if (request.Action != PAYMENT_CREATED)
        {
            return;
        }

        Payment payment = await paymentService.ValidateByUuidAsync(paymentUuid, cancellationToken);
        Domain.Order.Entities.Order order = await orderService.ValidateByIdAsync(payment.OrderId, cancellationToken);
        await paymentService.ConfirmAsync(order, payment, request.Id.ToString(), cancellationToken);
    }
 
    private void UpdatePaymentData(Payment payment, ProcessedPaymentDto paymentData)
    {
        payment.SetExternalId(paymentData.ExternalId);
        payment.SetUserPaymentCode(paymentData.UserPaymentCode);
        payment.SetStatus(paymentData.Status);
    }
}