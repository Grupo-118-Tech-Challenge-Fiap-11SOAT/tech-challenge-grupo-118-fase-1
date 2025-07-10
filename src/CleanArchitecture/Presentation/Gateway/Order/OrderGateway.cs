using Common.Enums;
using Common.Interfaces.Order.Gateway;
using Common.Interfaces.Order.Repositories;
using TechChallengeFastFood.CleanArch.Domain.Entities.Order.Entities;
using OrderDomain = TechChallengeFastFood.CleanArch.Domain.Entities.Order.Entities.Order;
using OrderEntity = Common.Dto.Order.Database.Order;

namespace TechChallengeFastFood.CleanArch.Presentation.Gateway.Order;

public class OrderGateway : IOrderGateway
{
    private readonly IOrderRepository _orderRepository;

    public OrderGateway(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<List<OrderDomain>?> GetAllAsync(OrderStatus status, CancellationToken cancellationToken = default,
        int skip = 0, int take = 10)
    {
        var orderEntities = await _orderRepository.GetAllAsync(status, cancellationToken, skip, take);

        if (orderEntities is null)
            return null;

        var orderDtos = new List<OrderDomain>();

        orderEntities.ForEach(orderEntity =>
        {
            var orderItems = CreateOrderItemsFromOrder(orderEntity);

            orderDtos.Add(new OrderDomain(orderEntity.OrderNumber,
                orderEntity.Cpf,
                orderEntity.Total,
                orderEntity.Status,
                orderEntity.IsActive,
                orderItems,
                orderEntity.Id,
                orderEntity.CreatedAt,
                orderEntity.UpdatedAt));
        });

        return orderDtos;
    }

    public async Task<List<OrderDomain>?> GetOrdersToMonitorAsync(CancellationToken cancellationToken = default, int skip = 0, int take = 10)
    {
        var orderEntities = await _orderRepository.GetOrdersToMonitorAsync(cancellationToken, skip, take);

        if (orderEntities is null)
            return null;

        var orderDtos = new List<OrderDomain>();

        orderEntities.ForEach(orderEntity =>
        {
            var orderItems = CreateOrderItemsFromOrder(orderEntity);

            orderDtos.Add(new OrderDomain(orderEntity.OrderNumber,
                orderEntity.Cpf,
                orderEntity.Total,
                orderEntity.Status,
                orderEntity.IsActive,
                orderItems,
                orderEntity.Id,
                orderEntity.CreatedAt,
                orderEntity.UpdatedAt));
        });

        return orderDtos;
    }

    public async Task<OrderDomain> CreateAsync(OrderDomain order, CancellationToken cancellationToken = default)
    {
        var orderEntity = CreateOrderEntityFromOrder(order);

        var createdOrder = await _orderRepository.CreateAsync(orderEntity, cancellationToken);

        order.Id = createdOrder.Id;

        return order;
    }

    public async Task<OrderDomain?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var orderEntity = await _orderRepository.GetByIdAsync(id, cancellationToken);

        if (orderEntity is null)
            return null;

        var orderItems = CreateOrderItemsFromOrder(orderEntity);

        return new OrderDomain(
            orderEntity.OrderNumber,
            orderEntity.Cpf,
            orderEntity.Total,
            orderEntity.Status,
            orderEntity.IsActive,
            orderItems,
            orderEntity.Id,
            orderEntity.CreatedAt,
            orderEntity.UpdatedAt);
    }

    public async Task<OrderDomain?> GetByIdWithPaymentAsync(int id, CancellationToken cancellationToken = default)
    {
        var orderEntity = await _orderRepository.GetByIdWithPaymentAsync(id, cancellationToken);

        if (orderEntity is null)
            return null;

        var orderItems = CreateOrderItemsFromOrder(orderEntity);

        var paymentDomain = new Domain.Entities.Payments.Entities.Payment(
            orderEntity.Payment.OrderId,
            orderEntity.Payment.Provider,
            orderEntity.Payment.Value,
            orderEntity.Payment.Id,
            orderEntity.Payment.ExternalId,
            orderEntity.Payment.UserPaymentCode);
        
        paymentDomain.SetStatus(orderEntity.Payment.Status);
        
        return new OrderDomain(
            orderEntity.OrderNumber,
            orderEntity.Cpf,
            orderEntity.Total,
            orderEntity.Status,
            orderEntity.IsActive,
            orderItems,
            orderEntity.Id,
            orderEntity.CreatedAt,
            orderEntity.UpdatedAt,
            paymentDomain);
    }
    
    public async Task<OrderDomain> UpdateAsync(OrderDomain order, CancellationToken cancellationToken = default)
    {
        var orderEntity = CreateOrderEntityFromOrder(order);

        await _orderRepository.UpdateAsync(orderEntity, cancellationToken);

        return order;
    }

    private OrderEntity CreateOrderEntityFromOrder(OrderDomain order)
    {
        var orderItemsEntity = order.OrderItems.Select(item =>
            new Common.Dto.Order.Database.OrderItem(item.ProductId, item.Quantity, order.Id)).ToList();

        var orderEntity = new OrderEntity(order.OrderNumber,
            order.Cpf,
            order.Total,
            order.Status,
            order.IsActive,
            orderItemsEntity,
            order.Id);

        orderEntity.UpdatedAt = DateTimeOffset.UtcNow;
        orderEntity.CreatedAt = order.CreatedAt;

        return orderEntity;
    }

    private List<OrderItem> CreateOrderItemsFromOrder(OrderEntity order)
    {
        var orderItems = new List<OrderItem>();

        order.OrderItems.ToList().ForEach(item =>
        {
            orderItems.Add(new OrderItem(item.ProductId, item.Quantity, order.Id));
        });

        return orderItems;
    }
}