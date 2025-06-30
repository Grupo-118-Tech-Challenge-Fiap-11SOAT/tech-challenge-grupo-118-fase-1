using System.Text.Json.Serialization;

namespace Common.Dto.MercadoPago;

public struct PaymentMercadoPagoDto
{
    [JsonPropertyName("in_store_order_id")]
    public string InStoreOrderId { get; set; }

    [JsonPropertyName("qr_data")]
    public string QrData { get; set; }
}