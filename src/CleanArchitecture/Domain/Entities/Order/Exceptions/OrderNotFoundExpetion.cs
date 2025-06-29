using TechChallengeFastFood.CleanArch.Domain.Entities.Base.Exceptions;

namespace TechChallengeFastFood.CleanArch.Domain.Entities.Order.Exceptions;

public class OrderNotFoundExpetion(int orderId) : DomainException
{
    public override string Message => $"Order {orderId} not found.";
}