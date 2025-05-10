namespace Infra.Database.SqlServer;

public class Employee
{
    public int Id { get; set; }
    
    public string Cpf { get; set; }
    
    public string Name { get; set; }
    
    public string Email { get; set; }
    
    public string Password { get; set; }
    
    //TODO: Change Role to enum
    public string Role { get; set; }
    
    public bool IsActive { get; set; }
}