using TechChallengeFastFood.CleanArch.Domain.Entities.Base.Exceptions;

namespace TechChallengeFastFood.CleanArch.Domain.Entities.Order.Exceptions;

public class ChangeStatusInvalidException : DomainException
{
    public override string Message => $"Current status is not recognized.";
}