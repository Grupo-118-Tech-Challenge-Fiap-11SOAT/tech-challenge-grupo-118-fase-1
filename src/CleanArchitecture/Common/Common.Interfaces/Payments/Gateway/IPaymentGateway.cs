using Common.Dto.Payments;
using PaymentDomain = TechChallengeFastFood.CleanArch.Domain.Entities.Payments.Entities.Payment;

namespace Common.Interfaces.Payments.Gateway;

public interface IPaymentGateway
{
    Task<ProcessedPaymentDto> ProcessPaymentAsync(PaymentDomain payment, CancellationToken cancellationToken);

    Task<PaymentDomain> CreatePaymentAsync(PaymentDomain payment, CancellationToken cancellationToken);

    Task<PaymentDomain> ConfirmPaymentAsync(PaymentDomain payment, CancellationToken cancellationToken);

    Task<PaymentDomain> GetPaymentByIdAsync(int id, CancellationToken cancellationToken);
}