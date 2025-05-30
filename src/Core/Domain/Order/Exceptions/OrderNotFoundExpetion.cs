namespace Domain.Order.Exceptions;

public class OrderNotFoundExpetion(int orderId) : DomainException
{
    public override string Message => $"Pedido {orderId} não encontrado.";
}

