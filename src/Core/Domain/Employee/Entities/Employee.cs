using Domain.Base.Entities;
using Domain.Employee.Ports;
using Domain.Employee.ValueObjects;

namespace Domain.Employee.Entities;

public class Employee : Person
{
    public string Password { get; set; }
    public EmployeeRole Role { get; set; }

    public async Task Save(IEmployeeRepository employeeRepository)
    {
        if (Id == 0)
        {
            await employeeRepository.Create(this);
        }
        else
        {
            await employeeRepository.Update(this);
        }
    }
}
