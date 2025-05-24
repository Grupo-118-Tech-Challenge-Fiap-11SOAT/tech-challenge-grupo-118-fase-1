using Domain.Payments.Dtos;
using Domain.Payments.Entities;
using Domain.Payments.Ports.In;
using Domain.Payments.Ports.Out;

namespace Application.Payments;

public class PaymentManager(IPaymentProcessorFactory factory, IPaymentRepository repository) : IPaymentManager
{
    public async Task<PaymentResponse> CreatePaymentAsync(PaymentRequest request, CancellationToken cancellationToken = default)
    {
        // TODO: validade the order in order service and recover it value;
        var payment = new Payment(request.OrderId, request.Provider, 100);

        IPaymentProcessor processor = factory.GetProcessor(payment.Provider);
        await processor.ProcessAsync(payment, cancellationToken);
        await repository.CreateAsync(payment, cancellationToken);

        return new PaymentResponse(payment);
    }
}