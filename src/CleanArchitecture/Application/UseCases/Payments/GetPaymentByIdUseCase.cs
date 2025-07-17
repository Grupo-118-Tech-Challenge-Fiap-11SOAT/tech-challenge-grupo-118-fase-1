using Common.Interfaces.Payments.Gateway;
using TechChallengeFastFood.CleanArch.Domain.Entities.Payments.Entities;

namespace TechChallengeFastFood.CleanArch.Application.UseCases.Payments;

public class GetPaymentByIdUseCase
{
    private readonly IPaymentGateway _paymentGateway;

    public GetPaymentByIdUseCase(IPaymentGateway paymentGateway)
    {
        _paymentGateway = paymentGateway;
    }

    public static GetPaymentByIdUseCase Create(IPaymentGateway paymentGateway)
    {
        return new GetPaymentByIdUseCase(paymentGateway);
    }

    public async Task<Payment> ExecuteAsync(int id, CancellationToken cancellationToken)
    {
        var payment = await _paymentGateway.GetPaymentByIdAsync(id, cancellationToken);
        return payment;
    }
}