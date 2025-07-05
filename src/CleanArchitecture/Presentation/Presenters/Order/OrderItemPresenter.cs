using Common.Dto.Order;
using Common.Interfaces.Order.Presenter;
using TechChallengeFastFood.CleanArch.Domain.Entities.Order.Entities;

namespace TechChallengeFastFood.CleanArch.Presentation.Presenters.Order;

public class OrderItemPresenter : IOrderItemPresenter
{
    public List<OrderItemDto> Convert(ICollection<OrderItem> orderItems)
    {
        var orderItemDtos = new List<OrderItemDto>();

        orderItems.ToList().ForEach(o => orderItemDtos.Add(Convert(o)));
        return orderItemDtos;
    }

    public OrderItemDto Convert(OrderItem orderItem)
    {
        return new OrderItemDto
        {
            ProductId = orderItem.ProductId,
            Quantity = orderItem.Quantity
        };
    }
}