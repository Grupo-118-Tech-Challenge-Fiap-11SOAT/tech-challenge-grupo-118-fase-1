
using Domain.Order.Entities;

namespace Domain.Order.Exceptions;

public class ChangeStatusInvalidException() : DomainException
{
    public override string Message => $"Status atual não é reconhecido.";
}

