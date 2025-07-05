using Common.Dto.Order;
using Common.Interfaces.Order.Presenter;

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
        var orderDtos = new List<OrderResponseDto>();

        orders.ForEach(o => orderDtos.Add(Convert(o)));
        return orderDtos;
    }

    public OrderResponseDto Convert(Domain.Entities.Order.Entities.Order order)
    {
        var items = _orderItemPresenter.Convert(order.OrderItems);

        return new OrderResponseDto(
            order.Id,
            order.OrderNumber,
            order.Cpf,
            order.Total,
            order.Status,
            items);
    }
}