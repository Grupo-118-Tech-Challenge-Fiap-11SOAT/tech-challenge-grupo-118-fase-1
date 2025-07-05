using Common.Interfaces.Order.Gateway;

namespace TechChallengeFastFood.CleanArch.Application.UseCases.Order;

public class GetOrderByIdUseCase
{
    private readonly IOrderGateway _orderGateway;

    public GetOrderByIdUseCase(IOrderGateway orderGateway)
    {
        _orderGateway = orderGateway;
    }

    public static GetOrderByIdUseCase Create(IOrderGateway orderGateway)
    {
        return new GetOrderByIdUseCase(orderGateway);
    }

    public async Task<Domain.Entities.Order.Entities.Order?> ExecuteAsync(int orderId,
        CancellationToken cancellationToken)
    {
        var order = await _orderGateway.GetByIdAsync(orderId, cancellationToken);
        return order;
    }
}