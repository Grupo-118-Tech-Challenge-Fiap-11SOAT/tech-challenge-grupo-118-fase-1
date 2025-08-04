namespace Common.Dto.Customers;

public class CustomerRequestDto
{
    public CustomerRequestDto()
    {
    }

    public CustomerRequestDto(
        string cpf,
        string name,
        string surname,
        string email,
        DateOnly birthDate)
    {
        Cpf = cpf;
        Name = name;
        Surname = surname;
        Email = email;
        BirthDate = birthDate;
    }

    public string Cpf { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public DateOnly BirthDate { get; set; }
}