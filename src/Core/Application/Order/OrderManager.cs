using Domain.Order.Dtos;
using Domain.Order.Entities;
using Domain.Order.Exceptions;
using Domain.Order.Ports.In;
using Domain.Order.Ports.Out;
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

        if (!await ValidateOrderItemsAsync(orderDto, cancellationToken))
            throw new Exception("Um ou mais produtos do pedido não existem ou estão inativos.");

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

        return new OrderDto(order);
    }

    public async Task<OrderDto> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(id, cancellationToken);
        var result = new OrderDto(order);

        return result;
    }

    private async Task<bool> ValidateOrderItemsAsync(OrderDto orderDto, CancellationToken cancellationToken)
    {
        int[] productIds = orderDto.Items.Select(i => i.ProductId).ToArray();
        var activeProducts = await _productManager.GetActiveProductsByIds(productIds, cancellationToken);

        var activeProductIds = activeProducts.Select(p => p.Id).ToHashSet();

        if (!productIds.All(id => activeProductIds.Contains(id)))
            return false;

        var productPriceById = activeProducts.ToDictionary(p => p.Id, p => p.Price);

        decimal total = 0;
        foreach (var item in orderDto.Items)
        {
            if (productPriceById.TryGetValue(item.ProductId, out var price))
            {
                total += item.Quantity * price;
            }
        }

        orderDto.Total = total;

        return true;
    }

}

