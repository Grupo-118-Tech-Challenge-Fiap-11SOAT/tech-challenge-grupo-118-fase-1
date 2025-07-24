using Common.Dto.Payments;
using PaymentDomain = TechChallengeFastFood.CleanArch.Domain.Entities.Payments.Entities.Payment;
using OrderDomain = TechChallengeFastFood.CleanArch.Domain.Entities.Order.Entities.Order;

namespace Common.Interfaces.Payments.Gateway;

public interface IPaymentGateway
{
    Task<ProcessedPaymentDto> ProcessPaymentAsync(PaymentDomain payment, OrderDomain order, CancellationToken cancellationToken);

    Task<PaymentDomain> CreatePaymentAsync(PaymentDomain payment, CancellationToken cancellationToken);

    Task<PaymentDomain> ConfirmPaymentAsync(PaymentDomain payment, CancellationToken cancellationToken);

    Task<PaymentDomain> GetPaymentByIdAsync(int id, CancellationToken cancellationToken);

    Task<PaymentDomain> GetPaymentByUuidAsync(Guid uuid, CancellationToken cancellationToken);
}