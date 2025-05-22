using Infra.Api.MercadoPago.Payments.Models;
using Infra.Api.MercadoPago.Payments.Repositories.Interfaces;
using Domain.Payments.Ports.Out;
using Refit;
using System;

namespace Infra.Api.MercadoPago.Payments.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly IPaymentMercadoPagoRepository _paymentMercadoPagoRepository;

        public PaymentRepository(IPaymentMercadoPagoRepository paymentMercadoPagoRepository)
        {
            _paymentMercadoPagoRepository = paymentMercadoPagoRepository;
        }

        public async Task CreateQrCodeAsync(CancellationToken cancellationToken = default)
        {

            var payment = new PaymentModel
            {
                ExternalReference = Guid.NewGuid().ToString(),
                Title = "Pedido de Lanche",
                Description = "Detalhes do pedido",
                NotificationUrl = "https://www.teste.com/url-webhook",
                TotalAmount = 50,
                Items = new List<ItemModel>
                {
                    new ItemModel
                    {
                        SkuNumber = "A123K9191938",
                        Category = "marketplace",
                        Title = "Hamburgão",
                        Description = "Hamburgão do bão",
                        UnitPrice = 50,
                        Quantity = 1,
                        UnitMeasure = "unit",
                        TotalAmount = 50
                    }
                },
                Sponsor = new SponsorModel
                {
                    Id = 1726345772
                },
                CashOut = new CashOutModel
                {
                    Amount = 0
                }
            };

            try
            {
                var paymentDto = await _paymentMercadoPagoRepository.CreateQrCodeAsync("407780263", "POS01", payment);
                Console.WriteLine($"QrCode created with id: {paymentDto.QrData}");
            }
            catch (ApiException ex)
            {
                Console.WriteLine($"Error: {ex.Content}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }

            
        }
    }
}