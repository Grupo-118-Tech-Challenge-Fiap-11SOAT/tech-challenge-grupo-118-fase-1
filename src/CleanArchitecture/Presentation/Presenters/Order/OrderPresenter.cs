using Common.Dto.Order;
using Common.Interfaces.Order.Presenter;
using TechChallengeFastFood.CleanArch.Domain.Entities.Payments.Entities;

namespace TechChallengeFastFood.CleanArch.Presentation.Presenters.Order;

public class OrderPresenter : IOrderPresenter
{
    private readonly IOrderItemPresenter _orderItemPresenter;

    public OrderPresenter(IOrderItemPresenter orderItemPresenter)
    {
        _orderItemPresenter = orderItemPresenter;
    }

    public List<OrderResponseDto> Convert(List<Domain.Entities.Order.Entities.Order> orders)
    {
        if (orders is null)
            return null;

        var orderDtos = new List<OrderResponseDto>();

        orders.ForEach(o => orderDtos.Add(Convert(o)));
        return orderDtos;
    }

    public OrderResponseDto Convert(Domain.Entities.Order.Entities.Order order)
    {
        if (order is null)
            return null;

        var items = _orderItemPresenter.Convert(order.OrderItems);

        return new OrderResponseDto(
            order.Id,
            order.OrderNumber,
            order.Cpf,
            order.Total,
            order.Status,
            items,
            order.CreatedAt);
    }

    public OrderPaymentResponseDto Convert(Domain.Entities.Order.Entities.Order order, Payment payment)
    {
        var items = _orderItemPresenter.Convert(order.OrderItems);

        if (order is null || payment is null)
            return null;

        return new OrderPaymentResponseDto(
            order.Id,
            order.OrderNumber,
            order.Cpf,
            order.Total,
            order.Status,
            items,
            order.CreatedAt,
            order.Payment?.Provider,
            order.Payment?.Status);
    }
}