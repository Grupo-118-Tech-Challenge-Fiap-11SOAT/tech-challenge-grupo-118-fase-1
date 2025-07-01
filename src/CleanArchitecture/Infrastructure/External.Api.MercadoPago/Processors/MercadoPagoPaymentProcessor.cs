using Common.Dto.MercadoPago;
using Common.Dto.Payments;
using Common.Enums;
using Common.Interfaces.Payments;
using External.Repositories.Interfaces;
using Microsoft.Extensions.Options;
using Refit;

namespace External.Processors;

public class MercadoPagoPaymentProcessor(
    IMercadoPagoRepository client,
    IOptions<MercadoPagoOptions> options) : IPaymentProcessor
{
    private readonly MercadoPagoOptions _options = options.Value;

    public async Task<ProcessedPaymentDto> ProcessAsync(PaymentExternalDto payment, CancellationToken cancellationToken = default)
    {
        try
        {
            PaymentMercadoPagoModel paymentModel = CreatePaymentModel(payment);

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
        
    private PaymentMercadoPagoModel CreatePaymentModel(PaymentExternalDto payment)
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