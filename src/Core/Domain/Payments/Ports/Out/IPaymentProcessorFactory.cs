using Domain.Payments.Enumerators;

namespace Domain.Payments.Ports.Out;

public interface IPaymentProcessorFactory
{
    /// <summary>
    /// Returns the payment processor for the specified payment provider.
    /// </summary>
    /// <param name="provider">The payment provider/param>
    /// <returns>The payment provider processor</returns>
    IPaymentProcessor GetProcessor(PaymentProvider provider);
}