using System.Text.Json.Serialization;

namespace Common.Dto.Payments;

public class MercadoPagoCallbackRequest
{
    [JsonPropertyName("action")]
    public string Action { get; set; }

    [JsonPropertyName("api_version")]
    public string ApiVersion { get; set; }

    [JsonPropertyName("data")]
    public MercadoPagoPaymentDataRequest Data { get; set; }

    [JsonPropertyName("date_created")]
    public DateTime DateCreated { get; set; }

    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("live_mode")]
    public bool LiveMode { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("user_id")]
    public string UserId { get; set; }
}

public class MercadoPagoPaymentDataRequest
{
    [JsonPropertyName("id")]
    public string Id { get; set; }
}