namespace Common.Dto.Customers;

public class CustomerUpdateDto
{
    public CustomerUpdateDto()
    {
    }

    public CustomerUpdateDto(
        int id,
        string cpf,
        string name,
        string surname,
        string email,
        DateOnly birthDate,
        bool isActive
    )
    {
        Id = id;
        Cpf = cpf;
        Name = name;
        Surname = surname;
        Email = email;
        BirthDate = birthDate;
        IsActive = isActive;
    }

    public int Id { get; set; }
    public string Cpf { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public DateOnly BirthDate { get; set; }
    public bool IsActive { get; set; }
}