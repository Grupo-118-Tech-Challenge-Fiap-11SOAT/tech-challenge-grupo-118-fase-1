namespace Infra.Api.MercadoPago.Payments.Options
{
    public class MercadoPagoOptions
    {
        public string BaseUrl { get; set; }
        public string AccessToken { get; set; }
        public string UserId { get; set; }
        public string PosId { get; set; }
        public long SponsorId { get; set; }
        public string NotificationUrl { get; set; }
    }
}