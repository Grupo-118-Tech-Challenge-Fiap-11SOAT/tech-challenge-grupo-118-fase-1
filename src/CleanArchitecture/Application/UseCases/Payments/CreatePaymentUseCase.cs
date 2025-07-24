using Common.Dto.Payments;
using Common.Interfaces.Order.Gateway;
using Common.Interfaces.Payments.Gateway;
using TechChallengeFastFood.CleanArch.Application.UseCases.Order;
using TechChallengeFastFood.CleanArch.Domain.Entities.Payments.Entities;

namespace TechChallengeFastFood.CleanArch.Application.UseCases.Payments;

public class CreatePaymentUseCase
{
    private readonly IPaymentGateway _paymentGateway;
    
    public CreatePaymentUseCase(IPaymentGateway paymentGateway)
    {
        _paymentGateway = paymentGateway;
    }

    public static CreatePaymentUseCase Create(IPaymentGateway paymentGateway)
    {
        return new CreatePaymentUseCase(paymentGateway);
    }

    public async Task<Payment> ExecuteAsync(
        Domain.Entities.Order.Entities.Order order,
        PaymentRequest paymentRequest,
        CancellationToken cancellationToken)
    {
        var payment = new Payment(paymentRequest.OrderId, paymentRequest.Provider, order.Total);

        var paymentData = await _paymentGateway.ProcessPaymentAsync(payment, order, cancellationToken);

        UpdatePaymentData(payment, paymentData);

        var createdPayment = await _paymentGateway.CreatePaymentAsync(payment, cancellationToken);

        return createdPayment;
    }

    private void UpdatePaymentData(Payment payment, ProcessedPaymentDto paymentData)
    {
        payment.SetExternalId(paymentData.ExternalId);
        payment.SetUserPaymentCode(paymentData.UserPaymentCode);
        payment.SetStatus(paymentData.Status);
    }
}