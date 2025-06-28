using TechChallengeFastFood.CleanArch.Infrastructure.Database.Base;

namespace TechChallengeFastFood.CleanArch.Infrastructure.Database.Employee.Entities;

public class Employee : Person
{
    public string Password { get; protected set; }
    public string Role { get; protected set; }

    public Employee(string cpf,
        string name,
        string surname,
        string email,
        DateOnly birthday,
        string password,
        string role,
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