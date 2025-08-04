using Common.Interfaces.Payments.Gateway;
using TechChallengeFastFood.CleanArch.Domain.Entities.Payments.Entities;

namespace TechChallengeFastFood.CleanArch.Application.UseCases.Payments;

public class GetPaymentByUuidUseCase
{
    private readonly IPaymentGateway _paymentGateway;

    public GetPaymentByUuidUseCase(IPaymentGateway paymentGateway)
    {
        _paymentGateway = paymentGateway;
    }

    public static GetPaymentByUuidUseCase Create(IPaymentGateway paymentGateway)
    {
        return new GetPaymentByUuidUseCase(paymentGateway);
    }

    public async Task<Payment> ExecuteAsync(Guid uuid, CancellationToken cancellationToken)
    {
        var payment = await _paymentGateway.GetPaymentByUuidAsync(uuid, cancellationToken);
        return payment;
    }
}