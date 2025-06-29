using TechChallengeFastFood.CleanArch.Domain.Entities.Base.Exceptions;

namespace TechChallengeFastFood.CleanArch.Domain.Entities.Order.Exceptions;

public class EmptyOrderItemsException : DomainException
{
    public override string Message => $"Cannot create an order with no items.";
}