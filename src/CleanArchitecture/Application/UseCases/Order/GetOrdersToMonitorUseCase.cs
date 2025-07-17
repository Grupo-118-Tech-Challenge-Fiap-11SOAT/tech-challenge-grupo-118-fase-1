using Common.Interfaces.Order.Gateway;

namespace TechChallengeFastFood.CleanArch.Application.UseCases.Order;

public class GetOrdersToMonitorUseCase
{
    private readonly IOrderGateway _orderGateway;

    public GetOrdersToMonitorUseCase(IOrderGateway orderGateway)
    {
        _orderGateway = orderGateway;
    }

    public static GetOrdersToMonitorUseCase Create(IOrderGateway orderGateway)
    {
        return new GetOrdersToMonitorUseCase(orderGateway);
    }

    public async Task<List<Domain.Entities.Order.Entities.Order>?> ExecuteAsync(int skip = 0,
        int take = 10,
        CancellationToken cancellationToken = default)
    {
        var orders = await _orderGateway.GetOrdersToMonitorAsync(cancellationToken, skip, take);
        return orders;
    }
}