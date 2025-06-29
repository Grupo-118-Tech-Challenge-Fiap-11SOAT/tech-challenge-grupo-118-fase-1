using Common.Dto.Order;
using TechChallengeFastFood.CleanArch.Domain.Entities.Base.Exceptions;

namespace TechChallengeFastFood.CleanArch.Domain.Entities.Order.Exceptions;

public class ChangeStatusNotAllowed(OrderStatus status) : DomainException
{
    public override string Message => $"It is not possible to change the status when it is as {status}";
}