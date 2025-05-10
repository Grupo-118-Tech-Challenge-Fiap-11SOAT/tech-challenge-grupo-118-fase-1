namespace Infra.Database.SqlServer;

public class Product
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    //TODO: Change Category to use the enumerator
    public string Category { get; set; }

    public decimal Price { get; set; }

    public bool IsActive { get; set; }
}