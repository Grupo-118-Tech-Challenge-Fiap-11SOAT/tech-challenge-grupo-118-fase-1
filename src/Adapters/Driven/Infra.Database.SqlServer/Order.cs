namespace Infra.Database.SqlServer;

public class Order
{
    public int Id { get; set; }

    public int DailySequentialId { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset UpdatedAt { get; set; }

    public string? Cpf { get; set; }

    public decimal Total { get; set; }

    //TODO: Change Status to use the enumerator
    public string Status { get; set; }
}