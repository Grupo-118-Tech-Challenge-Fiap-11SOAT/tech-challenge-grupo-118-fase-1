using Domain.Base.Entities;
using Domain.Employee.ValueObjects;

namespace Domain.Employee.Entities;

public class Employee : Person
{
    public string Password { get; set; }
    public EmployeeRole Role { get; set; }
}
