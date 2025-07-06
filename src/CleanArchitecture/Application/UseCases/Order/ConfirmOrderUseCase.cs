using Common.Interfaces.Order.Gateway;

namespace TechChallengeFastFood.CleanArch.Application.UseCases.Order;

public class ConfirmOrderUseCase
{
    private readonly IOrderGateway _orderGateway;

    public ConfirmOrderUseCase(IOrderGateway orderGateway)
    {
        _orderGateway = orderGateway;
    }

    public static ConfirmOrderUseCase Create(IOrderGateway orderGateway)
    {
        return new ConfirmOrderUseCase(orderGateway);
    }

    public async Task<Domain.Entities.Order.Entities.Order?> ExecuteAsync(Domain.Entities.Order.Entities.Order order,
        CancellationToken cancellationToken)
    {
        order.IsOrderOnReceivedStatus();

        order.ChangeStatus();

        return await _orderGateway.UpdateAsync(order, cancellationToken);
    }
}