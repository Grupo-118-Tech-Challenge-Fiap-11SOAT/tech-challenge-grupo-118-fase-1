using Common.Dto.Payments;
using Common.Enums;

namespace Common.Dto.Order;

public class OrderPaymentResponseDto : OrderResponseDto
{
    public PaymentProvider? Provider { get; set; }

    public PaymentStatus? Status { get; set; }

    public PaymentResponse Payment { get; set; }

    public OrderPaymentResponseDto(int id,
        int orderNumber,
        string? cpf,
        decimal total,
        OrderStatus status,
        List<OrderItemDto> items,
        DateTimeOffset? createdAt,
        PaymentProvider? paymentProvider,
        PaymentStatus? paymentStatus) : base(id, orderNumber, cpf, total, status, items, createdAt)
    {
        this.Provider = paymentProvider;
        this.Status = paymentStatus;
    }
}