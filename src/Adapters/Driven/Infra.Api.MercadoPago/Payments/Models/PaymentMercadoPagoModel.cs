using System.Text.Json.Serialization;

namespace Infra.Api.MercadoPago.Payments.Models
{
    public class PaymentMercadoPagoModel
    {
        [JsonPropertyName("external_reference")]
        public string ExternalReference { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("notification_url")]
        public string NotificationUrl { get; set; }

        [JsonPropertyName("total_amount")]
        public decimal TotalAmount { get; set; }

        [JsonPropertyName("items")]
        public List<ItemMercadoPagoModel> Items { get; set; }

        [JsonPropertyName("sponsor")]
        public SponsorMercadoPagoModel Sponsor { get; set; }

        [JsonPropertyName("cash_out")]
        public CashOutMercadoPagoModel CashOut { get; set; }
    }

    public class ItemMercadoPagoModel
    {
        [JsonPropertyName("sku_number")]
        public string SkuNumber { get; set; }

        [JsonPropertyName("category")]
        public string Category { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("unit_price")]
        public decimal UnitPrice { get; set; }

        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }

        [JsonPropertyName("unit_measure")]
        public string UnitMeasure { get; set; }

        [JsonPropertyName("total_amount")]
        public decimal TotalAmount { get; set; }
    }

    public class SponsorMercadoPagoModel
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }
    }

    public class CashOutMercadoPagoModel
    {
        [JsonPropertyName("amount")]
        public decimal Amount { get; set; }
    }
}
