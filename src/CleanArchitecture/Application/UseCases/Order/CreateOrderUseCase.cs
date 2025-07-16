using Common.Dto.Order;
using Common.Interfaces.Order.Gateway;
using Common.Interfaces.Products.Gateway;
using TechChallengeFastFood.CleanArch.Application.UseCases.Products;
using TechChallengeFastFood.CleanArch.Domain.Entities.Order.Entities;
using TechChallengeFastFood.CleanArch.Domain.Entities.Products.Entities;

namespace TechChallengeFastFood.CleanArch.Application.UseCases.Order;

public class CreateOrderUseCase
{
    private readonly IOrderGateway _orderGateway;

    public CreateOrderUseCase(IOrderGateway orderGateway)
    {
        _orderGateway = orderGateway;
    }

    public static CreateOrderUseCase Create(IOrderGateway orderGateway)
    {
        return new CreateOrderUseCase(orderGateway);
    }

    public async Task<Domain.Entities.Order.Entities.Order> ExecuteAsync(
        OrderRequestDto orderRequestDto,
        List<Product>? activeProducts,
        CancellationToken cancellationToken)
    {
        var orderItems = orderRequestDto.Items
            .Select(item => new OrderItem(item.ProductId, item.Quantity))
            .ToList();

        var order = new Domain.Entities.Order.Entities.Order(
            orderRequestDto.Cpf,
            orderItems,
            activeProducts);

        var createdOrder = await _orderGateway.CreateAsync(order, cancellationToken);

        return createdOrder;
    }
}