using Infra.Api.MercadoPago.Payments.Models;
using Infra.Api.MercadoPago.Payments.Repositories.Interfaces;
using Domain.Payments.Ports.Out;
using Domain.Payments.Entities;
using Infra.Api.MercadoPago.Payments.Dtos;
using Domain.Payments.Enumerators;
using Infra.Api.MercadoPago.Payments.Options;
using Microsoft.Extensions.Options;
using Refit;
using Domain.Payments.Dtos;
using Domain.Order.Entities;

namespace Infra.Api.MercadoPago.Payments.Processors
{
    public class MercadoPagoPaymentProcessor(
        IMercadoPagoRepository client,
        IOptions<MercadoPagoOptions> options) : IPaymentProcessor
    {
        private readonly MercadoPagoOptions _options = options.Value;
        private const string DEFAULT_UNIT = "https://api.mercadopago.com/";

        public async Task<ProcessedPaymentDto> ProcessAsync(Payment payment, Order order, CancellationToken cancellationToken = default)
        {
            try
            {
                PaymentMercadoPagoModel paymentModel = CreatePaymentModel(payment, order);

                PaymentMercadoPagoDto paymentDto = await client
                    .CreateQrCodeAsync(_options.UserId, _options.PosId, paymentModel);

                return new ProcessedPaymentDto
                {
                    ExternalId = paymentDto.InStoreOrderId,
                    Status = PaymentStatus.Pending,
                    UserPaymentCode = paymentDto.QrData
                };
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
        
        private PaymentMercadoPagoModel CreatePaymentModel(Payment payment, Order order)
        {
            return new PaymentMercadoPagoModel
            {
                ExternalReference = $"order_{order.Id}",
                Title = "Pedido de lanche",
                Description = "Pedido de lanche efetuado na loja TomeLanches",
                NotificationUrl = $"{_options.NotificationUrl}/{payment.Uuid}",
                TotalAmount = payment.Value,
                Items = order.OrderItems.Select(item => new ItemMercadoPagoModel
                {
                    Title = item.Product.Name,
                    Description = item.Product.Description,
                    UnitPrice = item.UnitPrice,
                    Quantity = item.Quantity,
                    UnitMeasure = DEFAULT_UNIT,
                    TotalAmount = item.TotalValue
                }).ToList(),
            };
        }
    }
}