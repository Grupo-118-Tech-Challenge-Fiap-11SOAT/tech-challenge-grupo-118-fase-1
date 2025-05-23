namespace Infra.Database.SqlServer;

public class Customer
{
    public int Id { get; set; }

    public string Cpf { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public DateTimeOffset BirthDate { get; set; }

    public DateTime CreatedAt { get; set; }

    public bool IsActive { get; set; }
}