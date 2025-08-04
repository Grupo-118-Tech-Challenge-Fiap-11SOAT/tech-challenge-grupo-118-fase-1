using Common.Dto.Payments;
using Common.Interfaces.Payments.Presenter;
using TechChallengeFastFood.CleanArch.Domain.Entities.Payments.Entities;

namespace TechChallengeFastFood.CleanArch.Presentation.Presenters.Payments;

public class PaymentPresenter : IPaymentPresenter
{
    public static IPaymentPresenter Create()
    {
        return new PaymentPresenter();
    }

    public PaymentResponse Convert(Payment payment)
    {
        return new PaymentResponse(payment.Id, payment.Uuid, payment.OrderId, payment.Provider, payment.Status,
            payment.UserPaymentCode);
    }
}