using Common.Dto.Order;
using TechChallengeFastFood.CleanArch.Domain.Entities.Payments.Entities;

namespace Common.Interfaces.Order.Presenter;

public interface IOrderPresenter
{
    List<OrderResponseDto> Convert(List<TechChallengeFastFood.CleanArch.Domain.Entities.Order.Entities.Order> orders);

    OrderResponseDto Convert(TechChallengeFastFood.CleanArch.Domain.Entities.Order.Entities.Order order);

    OrderPaymentResponseDto Convert(TechChallengeFastFood.CleanArch.Domain.Entities.Order.Entities.Order order, Payment payment);
}