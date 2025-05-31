using Domain.Order.Dtos;
using Domain.Order.Exceptions;
using Domain.Order.Ports.In;
using Domain.Order.Ports.Out;
using Domain.Order.ValueObjects;
using Domain.Products.Ports.In;


namespace Application.Order;

public class OrderManager : IOrderManager
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductManager _productManager;

    public OrderManager(IOrderRepository orderRepository, IProductManager productManager)
    {
        _orderRepository = orderRepository;
        _productManager = productManager;
    }


    public async Task<List<OrderResponseDto>> GetAllAsync(OrderStatus status, int skip, int take,
        CancellationToken cancellationToken)
    {
        var ordersList = await _orderRepository.GetAllAsync(status, cancellationToken, skip, take);

        var result = new List<OrderResponseDto>(ordersList.Count);

        foreach (var order in ordersList)
        {
            result.Add(new OrderResponseDto(order));
        }

        return result;
    }

    public async Task<OrderResponseDto> CreateAsync(OrderRequestDto orderDto, CancellationToken cancellationToken)
    {
        int[] productIds = orderDto.Items.Select(i => i.ProductId).ToArray();
        var activeProducts = await _productManager.GetActiveProductsByIds(productIds, cancellationToken);

        var order = new Domain.Order.Entities.Order(orderDto, activeProducts);

        await _orderRepository.CreateAsync(order, cancellationToken);

        var orderDtoResult = new OrderResponseDto
        {
            Cpf = orderDto.Cpf,
            Items = orderDto.Items,
            Total = order.Total,
            Status = order.Status,
            OrderNumber = order.OrderNumber,
            Id = order.Id
        };

        return orderDtoResult;
    }

    public async Task<OrderResponseDto> UpdateStatusAsync(int orderId, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(orderId, cancellationToken);

        if (order is null)
            throw new OrderNotFoundExpetion(orderId);

        order.ChangeStatus();
        await _orderRepository.UpdateAsync(order, cancellationToken);

        return new OrderResponseDto(order);
    }

    public async Task<OrderResponseDto> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(id, cancellationToken);
        var result = new OrderResponseDto(order);

        return result;
    }
}


