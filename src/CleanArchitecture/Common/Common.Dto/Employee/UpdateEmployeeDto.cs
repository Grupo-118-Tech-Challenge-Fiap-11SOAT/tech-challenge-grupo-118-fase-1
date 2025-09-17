using Common.Enums;

namespace Common.Dto.Employee;

public class UpdateEmployeeDto
{
    public int Id { get; set; }
    public string Cpf { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public DateOnly BirthDate { get; set; }
    public string Password { get; set; }
    public Roles Role { get; set; }
    public bool IsActive { get; set; }

    public UpdateEmployeeDto()
    {
    }

    public UpdateEmployeeDto(
        int id,
        string cpf,
        string name,
        string surname,
        string email,
        DateOnly birthDate,
        string password,
        Roles role,
        bool isActive)
    {
        Id = id;
        Cpf = cpf;
        Name = name;
        Surname = surname;
        Email = email;
        BirthDate = birthDate;
        Password = password;
        Role = role;
        IsActive = isActive;
    }
}