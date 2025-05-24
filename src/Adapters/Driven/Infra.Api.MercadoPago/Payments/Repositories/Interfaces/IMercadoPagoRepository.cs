using Refit;
using Infra.Api.MercadoPago.Payments.Models;
using Infra.Api.MercadoPago.Payments.Dtos;

namespace Infra.Api.MercadoPago.Payments.Repositories.Interfaces
{
    public interface IMercadoPagoRepository
    {
        [Post("/instore/orders/qr/seller/collectors/{user_id}/pos/{external_pos_id}/qrs")]
        Task<PaymentMercadoPagoDto> CreateQrCodeAsync(
            [AliasAs("user_id")] string userId,
            [AliasAs("external_pos_id")] string externalPosId,
            [Body] PaymentMercadoPagoModel payment);
    }
}