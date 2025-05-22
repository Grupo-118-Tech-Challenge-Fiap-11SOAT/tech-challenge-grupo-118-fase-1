using Domain.Employee.Exceptions;

namespace Domain.Order.Entities;

public class Order
{
    public int Id { get; set; }
    public int OrderNumber { get; set; }
    public string Cpf { get; set; }
    public double Total { get; set; }
    public OrderStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

}
