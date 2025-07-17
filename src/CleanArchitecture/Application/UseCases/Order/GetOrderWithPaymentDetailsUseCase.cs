using Common.Interfaces.Order.Gateway;

namespace TechChallengeFastFood.CleanArch.Application.UseCases.Order;

public class GetOrderWithPaymentDetailsUseCase
{
    private readonly IOrderGateway _orderGateway;

    public GetOrderWithPaymentDetailsUseCase(IOrderGateway orderGateway)
    {
        _orderGateway = orderGateway;
    }

    public static GetOrderWithPaymentDetailsUseCase Create(IOrderGateway orderGateway)
    {
        return new GetOrderWithPaymentDetailsUseCase(orderGateway);
    }

    public async Task<Domain.Entities.Order.Entities.Order?> ExecuteAsync(int orderId,
        CancellationToken cancellationToken)
    {
        var order = await _orderGateway.GetByIdWithPaymentAsync(orderId, cancellationToken);
        return order;
    }
}