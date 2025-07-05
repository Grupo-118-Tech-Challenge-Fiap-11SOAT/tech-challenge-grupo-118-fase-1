using Common.Interfaces.Order.Gateway;

namespace TechChallengeFastFood.CleanArch.Application.UseCases.Order;

public class UpdateOrderStatusUseCase
{
    private readonly IOrderGateway _orderGateway;
    private readonly GetOrderByIdUseCase _getOrderByIdUseCase;

    public UpdateOrderStatusUseCase(IOrderGateway orderGateway)
    {
        _orderGateway = orderGateway;
        _getOrderByIdUseCase = GetOrderByIdUseCase.Create(orderGateway);
    }

    public static UpdateOrderStatusUseCase Create(IOrderGateway orderGateway)
    {
        return new UpdateOrderStatusUseCase(orderGateway);
    }

    public async Task<Domain.Entities.Order.Entities.Order?> ExecuteAsync(int orderId,
        CancellationToken cancellationToken)
    {
        var order = await _getOrderByIdUseCase.ExecuteAsync(orderId, cancellationToken);

        if (order == null)
            return null;

        order.ChangeStatus();

        var updatedOrder = await _orderGateway.UpdateAsync(order, cancellationToken);
        return updatedOrder;
    }
}