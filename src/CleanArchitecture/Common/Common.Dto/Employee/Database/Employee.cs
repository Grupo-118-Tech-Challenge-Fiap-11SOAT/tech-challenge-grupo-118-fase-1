using Common.Dto.Base.Database;
using Common.Enums;

namespace Common.Dto.Employee.Database;

public class Employee : Person
{
    public string Password { get; protected set; }
    public Roles Role { get; protected set; }

    protected Employee()
    {
    }

    public Employee(int id)
    {
        Id = id;
    }

    public Employee(string cpf,
        string name,
        string surname,
        string email,
        DateOnly birthday,
        string password,
        Roles role,
        bool isActive,
        int id = 0)
    {
        this.Cpf = cpf;
        this.Name = name;
        this.Surname = surname;
        this.Email = email;
        this.BirthDay = birthday;
        this.Password = password;
        this.Role = role;
        this.IsActive = isActive;

        if (id != 0)
            Id = id;
    }
}