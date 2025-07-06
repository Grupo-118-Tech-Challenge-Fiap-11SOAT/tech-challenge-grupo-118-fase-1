using Common.Enums;
using Common.Interfaces.Order.Gateway;
using Common.Interfaces.Payments.Gateway;
using TechChallengeFastFood.CleanArch.Application.UseCases.Order;
using TechChallengeFastFood.CleanArch.Domain.Entities.Payments.Entities;

namespace TechChallengeFastFood.CleanArch.Application.UseCases.Payments;

public class ConfirmPaymentUseCase
{
    private readonly IPaymentGateway _paymentGateway;

    private readonly GetOrderByIdUseCase _getOrderByIdUseCase;
    private readonly ConfirmOrderUseCase _confirmOrderUseCase;

    public ConfirmPaymentUseCase(IPaymentGateway paymentGateway, IOrderGateway orderGateway)
    {
        _paymentGateway = paymentGateway;
        _getOrderByIdUseCase = GetOrderByIdUseCase.Create(orderGateway);
        _confirmOrderUseCase = ConfirmOrderUseCase.Create(orderGateway);
    }

    public static ConfirmPaymentUseCase Create(IPaymentGateway paymentGateway, IOrderGateway orderGateway)
    {
        return new ConfirmPaymentUseCase(paymentGateway, orderGateway);
    }

    public async Task<Payment> ExecuteAsync(int id, CancellationToken cancellationToken)
    {
        var payment = await _paymentGateway.GetPaymentByIdAsync(id, cancellationToken);
        var order = await _getOrderByIdUseCase.ExecuteAsync(payment.OrderId, cancellationToken);

        payment.IsPaymentOnPendingStatus();
        payment.SetStatusToApproved();

        var updatedPayment = await _paymentGateway.ConfirmPaymentAsync(payment, cancellationToken);
        await _confirmOrderUseCase.ExecuteAsync(order, cancellationToken);

        return updatedPayment;
    }
}