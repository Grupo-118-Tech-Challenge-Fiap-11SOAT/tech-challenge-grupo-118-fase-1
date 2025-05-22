using Domain.Payments.Ports.In;
using Domain.Payments.Ports.Out;

namespace Application.Payments;

public class PaymentManager : IPaymentManager
{
    private readonly IPaymentRepository _paymentRepository;

    public PaymentManager(IPaymentRepository paymentRepository)
    {
        _paymentRepository = paymentRepository;
    }

    public async Task CreatePaymentAsync(CancellationToken cancellationToken = default)
    {
        await _paymentRepository.CreateQrCodeAsync(cancellationToken);
    }
}