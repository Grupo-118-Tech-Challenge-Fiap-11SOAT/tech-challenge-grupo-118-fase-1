using Domain.Base.Entities;

namespace Domain.Customer.Entities;

public class Customer : Person
{
    public Customer(string name, string cpf, bool isAtive, DateTime birthDay, string email)
    {
        Cpf = cpf;
        Name = name;
        IsActive = isAtive;
        BirthDay = birthDay;
        Email = email;
    }
}