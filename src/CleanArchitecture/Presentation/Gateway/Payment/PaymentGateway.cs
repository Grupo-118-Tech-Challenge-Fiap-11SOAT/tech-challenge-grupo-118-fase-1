using Common.Dto.Payments;
using Common.Interfaces.Payments;
using Common.Interfaces.Payments.Gateway;
using Common.Interfaces.Payments.Repositories;
using PaymentDomain = TechChallengeFastFood.CleanArch.Domain.Entities.Payments.Entities.Payment;
using PaymentEntity = Common.Dto.Payments.Database.Payment;

namespace TechChallengeFastFood.CleanArch.Presentation.Gateway.Payment;

public class PaymentGateway : IPaymentGateway
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly IPaymentProcessorFactory _paymentProcessorFactory;

    public PaymentGateway(IPaymentRepository paymentRepository, IPaymentProcessorFactory paymentProcessorFactory)
    {
        _paymentRepository = paymentRepository;
        _paymentProcessorFactory = paymentProcessorFactory;
    }


    public async Task<ProcessedPaymentDto> ProcessPaymentAsync(Domain.Entities.Payments.Entities.Payment payment,
        CancellationToken cancellationToken)
    {
        var processor = _paymentProcessorFactory.GetProcessor(payment.Provider);

        var paymentExternalDto = new PaymentExternalDto(payment.Id,
            payment.Uuid,
            payment.OrderId,
            payment.Provider,
            payment.Status,
            payment.Value,
            payment.ExternalId,
            payment.UserPaymentCode);

        var paymentData = await processor.ProcessAsync(paymentExternalDto, cancellationToken);
        return paymentData;
    }

    public async Task<PaymentDomain> CreatePaymentAsync(
        Domain.Entities.Payments.Entities.Payment payment, CancellationToken cancellationToken)
    {
        var paymentEntity = new PaymentEntity(
            payment.Id,
            payment.Uuid,
            payment.OrderId,
            payment.Provider,
            payment.Status,
            payment.Value,
            payment.ExternalId,
            payment.UserPaymentCode);

        var createdPayment = await _paymentRepository.CreateAsync(paymentEntity, cancellationToken);

        payment.SetCreatedId(createdPayment.Id);

        return payment;
    }

    public async Task<PaymentDomain> ConfirmPaymentAsync(
        Domain.Entities.Payments.Entities.Payment payment, CancellationToken cancellationToken)
    {
        var paymentEntity = new PaymentEntity(
            payment.Id,
            payment.Uuid,
            payment.OrderId,
            payment.Provider,
            payment.Status,
            payment.Value,
            payment.ExternalId,
            payment.UserPaymentCode);

        await _paymentRepository.UpdateAsync(paymentEntity, cancellationToken);

        return payment;
    }

    public async Task<PaymentDomain> GetPaymentByIdAsync(int id,
        CancellationToken cancellationToken)
    {
        var payment = await _paymentRepository.GetByIdAsync(id, cancellationToken);

        return new PaymentDomain(
            payment.OrderId,
            payment.Provider,
            payment.Value,
            payment.Id,
            payment.ExternalId,
            payment.UserPaymentCode);
    }
}