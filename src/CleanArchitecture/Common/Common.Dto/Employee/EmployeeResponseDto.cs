using Common.Enums;

namespace Common.Dto.Employee;

public class EmployeeResponseDto
{
    
    public int Id { get; set; }
    public string Cpf { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public DateOnly BirthDate { get; set; }
    public Roles Role { get; set; }
    public bool IsActive { get; set; }
    public bool Error { get; set; }
    public string ErrorMessage { get; set; }
    
    public EmployeeResponseDto()
    {

    }

    public EmployeeResponseDto(
        int id,
        string cpf,
        string name,
        string surname,
        string email,
        DateOnly birthdate,
        Roles role,
        bool isActive)
    {
        Id = id;
        Cpf = cpf;
        Name = name;
        Surname = surname;
        Email = email;
        BirthDate = birthdate;
        Role = role;
        IsActive = isActive;
    }
    
}