using Common.Dto.Order;
using Common.Enums;
using Common.Interfaces.Order.Controller;
using Common.Interfaces.Order.Gateway;
using Common.Interfaces.Order.Presenter;
using Common.Interfaces.Products.Gateway;
using TechChallengeFastFood.CleanArch.Application.UseCases.Order;

namespace TechChallengeFastFood.CleanArch.Presentation.Controllers.Order;

public class OrderController : IOrderController
{
    private readonly CreateOrderUseCase _createOrderUseCase;
    private readonly GetAllOrdersUseCase _getAllOrdersUseCase;
    private readonly GetOrderByIdUseCase _getOrderByIdUseCase;
    private readonly UpdateOrderStatusUseCase _updateStatusOrderUseCase;

    private readonly IOrderPresenter _orderPresenter;

    public OrderController(IOrderGateway orderGateway, IProductGateway productGateway, IOrderPresenter orderPresenter)
    {
        _createOrderUseCase = CreateOrderUseCase.Create(orderGateway, productGateway);
        _getAllOrdersUseCase = GetAllOrdersUseCase.Create(orderGateway);
        _getOrderByIdUseCase = GetOrderByIdUseCase.Create(orderGateway);
        _updateStatusOrderUseCase = UpdateOrderStatusUseCase.Create(orderGateway);

        _orderPresenter = orderPresenter;
    }

    public async Task<List<OrderResponseDto>?> GetAllAsync(OrderStatus status,
        CancellationToken cancellationToken = default, int skip = 0, int take = 10)
    {
        var orders = await _getAllOrdersUseCase.ExecuteAsync(status, skip, take, cancellationToken);

        return orders is not null
            ? _orderPresenter.Convert(orders)
            : null;
    }

    public async Task<OrderResponseDto> CreateAsync(OrderRequestDto order,
        CancellationToken cancellationToken = default)
    {
        var createdOrder = await _createOrderUseCase.ExecuteAsync(order, cancellationToken);

        return _orderPresenter.Convert(createdOrder);
    }

    public async Task<OrderResponseDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var order = await _getOrderByIdUseCase.ExecuteAsync(id, cancellationToken);

        return order is not null
            ? _orderPresenter.Convert(order)
            : null;
    }

    public async Task<OrderResponseDto?> UpdateStatusAsync(int orderId, CancellationToken cancellationToken = default)
    {
        var updatedOrder = await _updateStatusOrderUseCase.ExecuteAsync(orderId, cancellationToken);

        return updatedOrder is not null ? _orderPresenter.Convert(updatedOrder) : null;
    }
}