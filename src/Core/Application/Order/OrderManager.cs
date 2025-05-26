using Domain.Order.Dtos;
using Domain.Order.Entities;
using Domain.Order.Exceptions;
using Domain.Order.Ports.In;
using Domain.Order.Ports.Out;


namespace Application.Order;

public class OrderManager : IOrderManager
{
    private readonly IOrderRepository _orderRepository;

    public OrderManager(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }


    public async Task<List<OrderDto>> GetAllAsync(OrderStatus status, int skip, int take, CancellationToken cancellationToken)
    {
        var ordersList = await _orderRepository.GetAllAsync(status, cancellationToken, skip, take);

        var result = new List<OrderDto>(ordersList.Count);

        foreach (var order in ordersList)
        {
            result.Add(new OrderDto(order));
        }

        return result;
    }

    public async Task<OrderDto> CreateAsync(OrderDto orderDto, CancellationToken cancellationToken)
    {

        var order = new Domain.Order.Entities.Order(orderDto);

        await _orderRepository.CreateAsync(order, cancellationToken);

        orderDto.Id = order.Id;

        return orderDto;

    }

    public async Task<OrderDto> UpdateStatusAsync(int orderId, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(orderId, cancellationToken);

        if (order is null)
            throw new OrderNotFoundExpetion(orderId);

        order.ChangeStatus();
        await _orderRepository.UpdateAsync(order, cancellationToken);

        return OrderDto.ToDto(order);
    }

    public async Task<OrderDto> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(id, cancellationToken);
        var result = OrderDto.ToDto(order);

        return result;
    }
}

