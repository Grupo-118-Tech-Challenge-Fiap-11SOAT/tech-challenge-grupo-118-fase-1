using System.ComponentModel;

namespace Common.Dto.Payments;

public enum PaymentProvider
{
    [Description("Pix Mercado Pago")]
    MercadoPago = 1,
}