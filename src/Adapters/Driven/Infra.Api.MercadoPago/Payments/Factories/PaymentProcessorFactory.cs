using Domain.Payments.Enumerators;
using Domain.Payments.Ports.Out;
using Infra.Api.MercadoPago.Payments.Processors;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.Api.MercadoPago.Payments.Factories
{
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
}