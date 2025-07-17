using Common.Dto.Base.Database;

namespace Common.Dto.Customers.Database;

public class Customer : Person
{
    protected Customer()
    {
    }

    public Customer(
        int id,
        string name,
        string cpf,
        string email)
    {
        Id = id;
        Name = name;
        Cpf = cpf;
        Email = email;
    }
}