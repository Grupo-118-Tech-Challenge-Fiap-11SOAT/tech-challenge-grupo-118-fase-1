using System.Text.Json.Serialization;

namespace Infra.Api.MercadoPago.Payments.Dtos
{
    public struct PaymentDto
    {
        [JsonPropertyName("in_store_order_id")]
        public string InStoreOrderId { get; set; }

        [JsonPropertyName("qr_data")]
        public string QrData { get; set; }
    }
}