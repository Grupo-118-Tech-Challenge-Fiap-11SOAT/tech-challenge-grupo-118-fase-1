using Infra.Api.MercadoPago.Payments.Models;
using Infra.Api.MercadoPago.Payments.Repositories.Interfaces;
using Domain.Payments.Ports.Out;
using Domain.Payments.Entities;
using Infra.Api.MercadoPago.Payments.Dtos;
using Domain.Payments.Enumerators;
using Infra.Api.MercadoPago.Payments.Options;
using Microsoft.Extensions.Options;
using Refit;

namespace Infra.Api.MercadoPago.Payments.Processors
{
    public class MercadoPagoPaymentProcessor(
        IMercadoPagoRepository client,
        IOptions<MercadoPagoOptions> options) : IPaymentProcessor
    {
        private readonly MercadoPagoOptions _options = options.Value;

        public async Task ProcessAsync(Payment payment, CancellationToken cancellationToken = default)
        {
            try
            {
                PaymentMercadoPagoModel paymentModel = CreatePaymentModel(payment);

                PaymentMercadoPagoDto paymentDto = await client
                    .CreateQrCodeAsync(_options.UserId, _options.PosId, paymentModel);

                payment.SetExternalId(paymentDto.InStoreOrderId);
                payment.SetUserPaymentCode(paymentDto.QrData);
                payment.SetStatus(PaymentStatus.Pending);
            }
            catch (ApiException ex)
            {
                Console.WriteLine($"Error: {ex.Content}");
                throw new Exception($"Error processing payment: {ex.Content}", ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }
        
        private PaymentMercadoPagoModel CreatePaymentModel(Payment payment)
        {
            return new PaymentMercadoPagoModel
            {
                ExternalReference = payment.Uuid.ToString(),
                Title = "Pedido de lanche",
                Description = "Pedido de lanche efetuado na loja TomeLanches",
                NotificationUrl = _options.NotificationUrl,
                TotalAmount = payment.Value,
                Sponsor = new SponsorMercadoPagoModel
                {
                    Id = _options.SponsorId
                },
                CashOut = new CashOutMercadoPagoModel
                {
                    Amount = payment.Value
                }
            };
        }
    }
}