using Common.Dto.Order;
using Common.Enums;
using Common.Interfaces.Order.Controller;
using Common.Interfaces.Order.Gateway;
using Common.Interfaces.Order.Presenter;
using Common.Interfaces.Order.Repositories;
using Common.Interfaces.Products.Gateway;
using Common.Interfaces.Products.Repositories;
using TechChallengeFastFood.CleanArch.Application.UseCases.Order;
using TechChallengeFastFood.CleanArch.Application.UseCases.Products;
using TechChallengeFastFood.CleanArch.Presentation.Gateway.Order;
using TechChallengeFastFood.CleanArch.Presentation.Gateway.Products;
using TechChallengeFastFood.CleanArch.Presentation.Presenters.Order;

namespace TechChallengeFastFood.CleanArch.Presentation.Controllers.Order;

public class OrderController : IOrderController
{
    private readonly CreateOrderUseCase _createOrderUseCase;
    private readonly GetAllOrdersUseCase _getAllOrdersUseCase;
    private readonly GetOrdersToMonitorUseCase _getOrdersToMonitorUseCase;
    private readonly GetOrderByIdUseCase _getOrderByIdUseCase;
    private readonly UpdateOrderStatusUseCase _updateStatusOrderUseCase;
    private readonly GetOrderWithPaymentDetailsUseCase _getOrderWithPaymentDetailsUseCase;

    private readonly GetActiveProductsByIdsUseCase _getActiveProductsByIdsUseCase;

    private readonly IOrderPresenter _orderPresenter;

    public OrderController(IOrderRepository orderRepository, IProductRepository productRepository)
    {
        IOrderGateway orderGateway = OrderGateway.Create(orderRepository);
        IProductGateway productGateway = ProductGateway.Create(productRepository);

        _createOrderUseCase = CreateOrderUseCase.Create(orderGateway);
        _getAllOrdersUseCase = GetAllOrdersUseCase.Create(orderGateway);
        _getOrdersToMonitorUseCase = GetOrdersToMonitorUseCase.Create(orderGateway);
        _getOrderByIdUseCase = GetOrderByIdUseCase.Create(orderGateway);
        _updateStatusOrderUseCase = UpdateOrderStatusUseCase.Create(orderGateway);
        _getOrderWithPaymentDetailsUseCase = GetOrderWithPaymentDetailsUseCase.Create(orderGateway);

        _getActiveProductsByIdsUseCase = GetActiveProductsByIdsUseCase.Create(productGateway);

        _orderPresenter = OrderPresenter.Create(OrderItemPresenter.Create());
    }

    public static IOrderController Create(IOrderRepository orderRepository, IProductRepository productRepository)
    {
        return new OrderController(orderRepository, productRepository);
    }

    public async Task<List<OrderResponseDto>?> GetAllAsync(OrderStatus status,
        CancellationToken cancellationToken = default, int skip = 0, int take = 10)
    {
        var orders = await _getAllOrdersUseCase.ExecuteAsync(status, skip, take, cancellationToken);

        return orders is not null
            ? _orderPresenter.Convert(orders)
            : null;
    }

    public async Task<List<OrderResponseDto>?> GetOrdersToMonitorAsync(CancellationToken cancellationToken = default,
        int skip = 0, int take = 10)
    {
        {
            var orders = await _getOrdersToMonitorUseCase.ExecuteAsync(skip, take, cancellationToken);

            return orders is not null
                ? _orderPresenter.Convert(orders)
                : null;
        }
    }

    public async Task<OrderResponseDto> CreateAsync(OrderRequestDto order,
        CancellationToken cancellationToken = default)
    {
        int[] productIds = order
            .Items
            .Select(item => item.ProductId)
            .ToArray();

        var activeProducts = await _getActiveProductsByIdsUseCase.ExecuteAsync(productIds, cancellationToken);

        var createdOrder = await _createOrderUseCase.ExecuteAsync(order, activeProducts, cancellationToken);

        return _orderPresenter.Convert(createdOrder);
    }

    public async Task<OrderResponseDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var order = await _getOrderByIdUseCase.ExecuteAsync(id, cancellationToken);

        return order is not null
            ? _orderPresenter.Convert(order)
            : null;
    }

    public async Task<OrderPaymentResponseDto> GetByIdWithPaymentAsync(int id,
        CancellationToken cancellationToken = default)
    {
        var orderWithPayment = await _getOrderWithPaymentDetailsUseCase.ExecuteAsync(id, cancellationToken);

        return _orderPresenter.Convert(orderWithPayment, orderWithPayment.Payment);
    }

    public async Task<OrderResponseDto?> UpdateStatusAsync(int orderId,
        CancellationToken cancellationToken = default)
    {
        var updatedOrder = await _updateStatusOrderUseCase.ExecuteAsync(orderId, cancellationToken);

        return updatedOrder is not null ? _orderPresenter.Convert(updatedOrder) : null;
    }
}