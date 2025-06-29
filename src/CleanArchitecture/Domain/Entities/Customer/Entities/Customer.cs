using TechChallengeFastFood.CleanArch.Domain.Entities.Base;
using TechChallengeFastFood.CleanArch.Domain.Entities.Base.Entities;
using TechChallengeFastFood.CleanArch.Domain.Entities.Base.Exceptions;
using TechChallengeFastFood.CleanArch.Domain.Entities.Base.Extensions;

namespace TechChallengeFastFood.CleanArch.Domain.Entities.Customer.Entities;

public class Customer : Person
{
    public Customer()
    {
    }

    public Customer(string cpf,
        string name,
        string surname,
        string email,
        DateOnly birthday,
        bool isActive,
        int id = 0)
    {
        Cpf = cpf.SanitizeCpf();
        Name = name;
        Surname = surname;
        Email = email;
        BirthDay = birthday;
        IsActive = isActive;
        UpdatedAt = DateTimeOffset.Now;

        if (id != 0)
            Id = id;
        
        ValidateCustomer();
    }
    
    public void UpdateCustomer(string cpf,
        string name,
        string surname,
        string email,
        DateOnly birthday,
        bool isActive)
    {
        Cpf = cpf;
        Name = name;
        Surname = surname;
        Email = email;
        BirthDay = birthday;
        IsActive = isActive;
        UpdatedAt = DateTimeOffset.Now;

        ValidateCustomer();
    }
    
    private void ValidateCustomer()
    {
        if (!Cpf.IsValidCpf())
        {
            throw new InvalidCpfException();
        }

        if (!Email.IsValidEmail())
        {
            throw new InvalidEmailException();
        }
    }
}