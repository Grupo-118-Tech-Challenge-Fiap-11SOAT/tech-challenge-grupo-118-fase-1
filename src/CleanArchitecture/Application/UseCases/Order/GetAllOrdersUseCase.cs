using Common.Enums;
using Common.Interfaces.Order.Gateway;

namespace TechChallengeFastFood.CleanArch.Application.UseCases.Order;

public class GetAllOrdersUseCase
{
    private readonly IOrderGateway _orderGateway;

    public GetAllOrdersUseCase(IOrderGateway orderGateway)
    {
        _orderGateway = orderGateway;
    }

    public static GetAllOrdersUseCase Create(IOrderGateway orderGateway)
    {
        return new GetAllOrdersUseCase(orderGateway);
    }

    public async Task<List<Domain.Entities.Order.Entities.Order>?> ExecuteAsync(OrderStatus status,
        int skip = 0,
        int take = 10,
        CancellationToken cancellationToken = default)
    {
        var orders = await _orderGateway.GetAllAsync(status, cancellationToken, skip, take);
        return orders;
    }
}