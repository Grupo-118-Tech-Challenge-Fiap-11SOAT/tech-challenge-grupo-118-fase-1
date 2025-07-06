using Common.Dto.Payments;
using Common.Interfaces.Order.Gateway;
using Common.Interfaces.Payments.Gateway;
using TechChallengeFastFood.CleanArch.Application.UseCases.Order;
using TechChallengeFastFood.CleanArch.Domain.Entities.Payments.Entities;

namespace TechChallengeFastFood.CleanArch.Application.UseCases.Payments;

public class CreatePaymentUseCase
{
    private readonly IPaymentGateway _paymentGateway;

    private readonly GetOrderByIdUseCase _getOrderByIdUseCase;

    public CreatePaymentUseCase(IPaymentGateway paymentGateway, IOrderGateway orderGateway)
    {
        _paymentGateway = paymentGateway;
        _getOrderByIdUseCase = GetOrderByIdUseCase.Create(orderGateway);
    }

    public static CreatePaymentUseCase Create(IPaymentGateway paymentGateway, IOrderGateway orderGateway)
    {
        return new CreatePaymentUseCase(paymentGateway, orderGateway);
    }

    public async Task<Payment> ExecuteAsync(PaymentRequest paymentRequest, CancellationToken cancellationToken)
    {
        var order = await _getOrderByIdUseCase.ExecuteAsync(paymentRequest.OrderId, cancellationToken);

        var payment = new Payment(paymentRequest.OrderId, paymentRequest.Provider, order.Total);

        var paymentData = await _paymentGateway.ProcessPaymentAsync(payment, cancellationToken);

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