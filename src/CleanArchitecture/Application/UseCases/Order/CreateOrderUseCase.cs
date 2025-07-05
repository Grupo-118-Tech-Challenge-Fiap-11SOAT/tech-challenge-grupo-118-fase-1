using Common.Dto.Order;
using Common.Interfaces.Order.Gateway;
using Common.Interfaces.Products.Gateway;
using TechChallengeFastFood.CleanArch.Application.UseCases.Products;
using TechChallengeFastFood.CleanArch.Domain.Entities.Order.Entities;

namespace TechChallengeFastFood.CleanArch.Application.UseCases.Order;

public class CreateOrderUseCase
{
    private readonly IOrderGateway _orderGateway;

    private readonly GetActiveProductsByIdsUseCase _getActiveProductsByIdsUseCase;

    public CreateOrderUseCase(IOrderGateway orderGateway, IProductGateway productGateway)
    {
        _orderGateway = orderGateway;
        _getActiveProductsByIdsUseCase = GetActiveProductsByIdsUseCase.Create(productGateway);
    }

    public static CreateOrderUseCase Create(IOrderGateway orderGateway, IProductGateway productGateway)
    {
        return new CreateOrderUseCase(orderGateway, productGateway);
    }

    public async Task<Domain.Entities.Order.Entities.Order> ExecuteAsync(OrderRequestDto orderRequestDto,
        CancellationToken cancellationToken)
    {
        int[] productIds = orderRequestDto
            .Items
            .Select(item => item.ProductId)
            .ToArray();

        var activeProducts = await _getActiveProductsByIdsUseCase.ExecuteAsync(productIds, cancellationToken);

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