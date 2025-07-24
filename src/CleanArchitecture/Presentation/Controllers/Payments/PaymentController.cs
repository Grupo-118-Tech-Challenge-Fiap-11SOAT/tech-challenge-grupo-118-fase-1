using Common.Dto.Payments;
using Common.Interfaces.Order.Gateway;
using Common.Interfaces.Order.Repositories;
using Common.Interfaces.Payments;
using Common.Interfaces.Payments.Controller;
using Common.Interfaces.Payments.Gateway;
using Common.Interfaces.Payments.Presenter;
using Common.Interfaces.Payments.Repositories;
using TechChallengeFastFood.CleanArch.Application.UseCases.Order;
using TechChallengeFastFood.CleanArch.Application.UseCases.Payments;
using TechChallengeFastFood.CleanArch.Presentation.Gateway.Order;
using TechChallengeFastFood.CleanArch.Presentation.Gateway.Payment;
using TechChallengeFastFood.CleanArch.Presentation.Presenters.Payments;
using PaymentResponse = Common.Dto.Payments.PaymentResponse;

namespace TechChallengeFastFood.CleanArch.Presentation.Controllers.Payments;

public class PaymentController : IPaymentController
{
    private readonly CreatePaymentUseCase _createPaymentUseCase;
    private readonly ConfirmPaymentUseCase _confirmPaymentUseCase;
    private readonly GetPaymentByIdUseCase _getPaymentByIdUseCase;
    private readonly GetOrderByIdUseCase _getOrderByIdUseCase;
    private readonly GetPaymentByUuidUseCase _getPaymentByUuidUseCase;
    private readonly IPaymentPresenter _paymentPresenter;

    private const string PAYMENT_CREATED = "payment.created";

    public PaymentController(IPaymentRepository paymentRepository,
        IOrderRepository orderRepository,
        IPaymentProcessorFactory paymentProcessorFactory)
    {
        IOrderGateway orderGateway = OrderGateway.Create(orderRepository);
        IPaymentGateway paymentGateway = PaymentGateway.Create(paymentRepository, paymentProcessorFactory);

        _createPaymentUseCase = CreatePaymentUseCase.Create(paymentGateway);
        _confirmPaymentUseCase = ConfirmPaymentUseCase.Create(paymentGateway, orderGateway);
        _getPaymentByIdUseCase = GetPaymentByIdUseCase.Create(paymentGateway);
        _getOrderByIdUseCase = GetOrderByIdUseCase.Create(orderGateway);
        _getPaymentByUuidUseCase = GetPaymentByUuidUseCase.Create(paymentGateway);
        _paymentPresenter = PaymentPresenter.Create();
    }

    public static IPaymentController Create(IPaymentRepository paymentRepository,
        IOrderRepository orderRepository,
        IPaymentProcessorFactory paymentProcessorFactory)
    {
        return new PaymentController(paymentRepository,
            orderRepository,
            paymentProcessorFactory);
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

    public async Task ProcessCallbackAsync(Guid uuid, MercadoPagoCallbackRequest request, CancellationToken cancellationToken)
    {
        if (request.Action != PAYMENT_CREATED)
            return;

        var payment = await _getPaymentByUuidUseCase.ExecuteAsync(uuid, cancellationToken);
        var order = await _getOrderByIdUseCase.ExecuteAsync(payment.OrderId, cancellationToken);
        await _confirmPaymentUseCase.ExecuteAsync(payment, order, cancellationToken);
    }
}