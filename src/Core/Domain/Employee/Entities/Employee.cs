using Domain.Base.Entities;
using Domain.Base.Exceptions;
using Domain.Base.Extensions;
using Domain.Employee.Exceptions;
using Domain.Employee.ValueObjects;

namespace Domain.Employee.Entities;

public class Employee : Person
{
    public string Password { get; set; }
    public EmployeeRole Role { get; set; }

    public void ValidateEmployee()
    {
        if (string.IsNullOrEmpty(Cpf))
        {
            throw new CpfNullOrEmptyException();
        }

        if (!Cpf.IsValidCpf())
        {
            throw new InvalidCpfException();
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

        if (!Email.IsValidEmail())
        {
            throw new InvalidEmailException();
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
