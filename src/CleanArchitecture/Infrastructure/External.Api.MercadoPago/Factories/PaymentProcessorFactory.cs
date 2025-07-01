using Common.Dto.Payments;
using Common.Enums;
using Common.Interfaces.Payments;
using External.Processors;
using Microsoft.Extensions.DependencyInjection;

namespace External.Factories;

public class PaymentProcessorFactory(IServiceProvider serviceProvider) : IPaymentProcessorFactory
{
    public IPaymentProcessor GetProcessor(PaymentProvider provider)
    {
        return provider switch
        {
            PaymentProvider.MercadoPago => serviceProvider.GetRequiredService<MercadoPagoPaymentProcessor>(),
            _ => throw new NotImplementedException($"Processor for {provider} not implemented.")
        };
    }
}