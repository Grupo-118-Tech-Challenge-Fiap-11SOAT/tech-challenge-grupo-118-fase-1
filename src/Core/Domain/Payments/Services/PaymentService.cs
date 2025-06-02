using Domain.Base.Exceptions;
using Domain.Payments.Entities;
using Domain.Payments.Ports.Out;
using Domain.Payments.Services.Interfaces;
using Domain.Payments.Enumerators;
using Domain.Order.Services.Interfaces;

namespace Domain.Payments.Services;

public class PaymentService(IPaymentRepository repository, IOrderService orderService) : IPaymentService
{
    public async Task<Payment> ValidateByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var payment = await repository.GetByIdAsync(id, cancellationToken);
        return payment ?? throw new DomainException($"Order with id {id} not found.");
    }

    public async Task ConfirmAsync(Order.Entities.Order order, Payment payment, CancellationToken cancellationToken = default)
    {
        if (payment.Status != PaymentStatus.Pending)
            throw new DomainException($"Payment with id {payment.Id} is not in a pending state.");

        payment.SetStatus(PaymentStatus.Approved);
        await repository.UpdateAsync(payment, cancellationToken);

        await orderService.ConfirmAsync(order, cancellationToken);
    }
}