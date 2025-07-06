using Common.Dto.Payments;
using TechChallengeFastFood.CleanArch.Domain.Entities.Payments.Entities;

namespace Common.Interfaces.Payments.Presenter;

public interface IPaymentPresenter
{
    PaymentResponse Convert(Payment payment);
}