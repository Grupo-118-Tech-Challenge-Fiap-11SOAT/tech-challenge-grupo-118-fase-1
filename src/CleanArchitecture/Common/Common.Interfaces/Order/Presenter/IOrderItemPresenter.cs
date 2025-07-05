using Common.Dto.Order;

namespace Common.Interfaces.Order.Presenter;

public interface IOrderItemPresenter
{
    List<OrderItemDto> Convert(
        ICollection<TechChallengeFastFood.CleanArch.Domain.Entities.Order.Entities.OrderItem> orderItems);

    OrderItemDto Convert(TechChallengeFastFood.CleanArch.Domain.Entities.Order.Entities.OrderItem orderItem);
}