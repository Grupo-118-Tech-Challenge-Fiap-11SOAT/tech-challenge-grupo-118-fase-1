using Domain.Base.Entities;
using Domain.Employee.Exceptions;
using Domain.Employee.Ports.Out;
using Domain.Employee.ValueObjects;

namespace Domain.Employee.Entities;

public class Employee : Person
{
    public string Password { get; set; }
    public EmployeeRole Role { get; set; }

    public async Task Save(IEmployeeRepository employeeRepository)
    {
        ValidateEmployee();

        if (Id == 0)
        {
            await employeeRepository.Create(this);
        }
        else
        {
            await employeeRepository.Update(this);
        }
    }

    private void ValidateEmployee()
    {
        if (string.IsNullOrEmpty(Cpf))
        {
            throw new CpfNullOrEmptyException();
        }

        if (string.IsNullOrEmpty(Name))
        {
            throw new NameNullOrEmptyException();
        }

        if (string.IsNullOrEmpty(Surname))
        {
            throw new SurnameNullOrEmptyException();
        }

        if (string.IsNullOrEmpty(Email))
        {
            throw new EmailNullOrEmptyException();
        }

        if (BirthDay == DateTime.MinValue)
        {
            throw new BirthDayMinValueException();
        }

        if (string.IsNullOrEmpty(Password))
        {
            throw new PasswordNullOrEmptyException();
        }
    }
}
