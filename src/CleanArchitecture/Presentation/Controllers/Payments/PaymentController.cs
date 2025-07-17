using Common.Dto.Payments;
using Common.Interfaces.Order.Gateway;
using Common.Interfaces.Payments.Controller;
using Common.Interfaces.Payments.Gateway;
using Common.Interfaces.Payments.Presenter;
using TechChallengeFastFood.CleanArch.Application.UseCases.Order;
using TechChallengeFastFood.CleanArch.Application.UseCases.Payments;
using TechChallengeFastFood.CleanArch.Presentation.Gateway.Payment;
using PaymentCallbackRequest = Common.Dto.Payments.PaymentCallbackRequest;
using PaymentResponse = Common.Dto.Payments.PaymentResponse;

namespace TechChallengeFastFood.CleanArch.Presentation.Controllers.Payments;

public class PaymentController : IPaymentController
{
    private readonly CreatePaymentUseCase _createPaymentUseCase;
    private readonly ConfirmPaymentUseCase _confirmPaymentUseCase;
    private readonly GetPaymentByIdUseCase _getPaymentByIdUseCase;

    private readonly GetOrderByIdUseCase _getOrderByIdUseCase;

    private readonly IPaymentPresenter _paymentPresenter;

    public PaymentController(IPaymentGateway paymentGateway, IOrderGateway orderGateway,
        IPaymentPresenter paymentPresenter)
    {
        _createPaymentUseCase = CreatePaymentUseCase.Create(paymentGateway);
        _confirmPaymentUseCase = ConfirmPaymentUseCase.Create(paymentGateway, orderGateway);
        _getPaymentByIdUseCase = GetPaymentByIdUseCase.Create(paymentGateway);

        _getOrderByIdUseCase = GetOrderByIdUseCase.Create(orderGateway);

        _paymentPresenter = paymentPresenter;
    }

    public async Task<PaymentResponse> CreatePaymentAsync(PaymentRequest paymentRequest,
        CancellationToken cancellationToken)
    {
        var order = await _getOrderByIdUseCase.ExecuteAsync(paymentRequest.OrderId, cancellationToken);

        var payment = await _createPaymentUseCase.ExecuteAsync(order, paymentRequest, cancellationToken);

        return _paymentPresenter.Convert(payment);
    }

    public async Task<PaymentResponse> ConfirmPaymentAsync(int id, CancellationToken cancellationToken)
    {
        var payment = await _getPaymentByIdUseCase.ExecuteAsync(id, cancellationToken);
        var order = await _getOrderByIdUseCase.ExecuteAsync(payment.OrderId, cancellationToken);

        var updatedPayment = await _confirmPaymentUseCase.ExecuteAsync(payment, order, cancellationToken);
        return _paymentPresenter.Convert(updatedPayment);
    }

    public async Task ProcessCallbackAsync(PaymentCallbackRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}