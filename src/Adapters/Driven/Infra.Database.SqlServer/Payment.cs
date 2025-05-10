namespace Infra.Database.SqlServer;

public class Payment
{
    public int Id { get; set; }

    public string CopyAndPasteId { get; set; }

    //TODO: Change Status to enum
    public string Status { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset UpdatedAt { get; set; }

    public int OrderId { get; set; }
}