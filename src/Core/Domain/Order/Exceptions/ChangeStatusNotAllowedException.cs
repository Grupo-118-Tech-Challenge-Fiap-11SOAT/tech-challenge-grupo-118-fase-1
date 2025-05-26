
using Domain.Order.Entities;

namespace Domain.Order.Exceptions;

class ChangeStatusNotAllowed(OrderStatus status) : DomainException
{
    public override string Message => $"Não é possível alterar o status quando ele está como {status}";
}

