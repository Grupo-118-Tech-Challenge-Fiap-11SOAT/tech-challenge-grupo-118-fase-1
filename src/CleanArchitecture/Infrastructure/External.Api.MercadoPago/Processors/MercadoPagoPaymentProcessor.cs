using Common.Dto.MercadoPago;
using Common.Dto.Payments;
using Common.Enums;
using Common.Interfaces.Payments;
using External.Repositories.Interfaces;
using Microsoft.Extensions.Options;
using Refit;
using OrderDomain = TechChallengeFastFood.CleanArch.Domain.Entities.Order.Entities.Order;

namespace External.Processors;

public class MercadoPagoPaymentProcessor(
    IMercadoPagoRepository client,
    IOptions<MercadoPagoOptions> options) : IPaymentProcessor
{
    private readonly MercadoPagoOptions _options = options.Value;

    public async Task<ProcessedPaymentDto> ProcessAsync(PaymentExternalDto payment, OrderDomain order, CancellationToken cancellationToken = default)
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
    
    private PaymentMercadoPagoModel CreatePaymentModel(PaymentExternalDto payment, OrderDomain order)
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
                    UnitMeasure = "unit",
                    TotalAmount = item.TotalValue
                }).ToList(),
            };
        }
}