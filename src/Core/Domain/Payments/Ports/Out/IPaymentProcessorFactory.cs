using Domain.Payments.Enumerators;

namespace Domain.Payments.Ports.Out;

public interface IPaymentProcessorFactory
{
    IPaymentProcessor GetProcessor(PaymentProvider provider);
}