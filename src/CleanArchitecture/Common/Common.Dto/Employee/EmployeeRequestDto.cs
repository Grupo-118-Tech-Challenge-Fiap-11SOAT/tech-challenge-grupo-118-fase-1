namespace Common.Dto.Employee;

public class EmployeeRequestDto
{
    public string Cpf { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public DateOnly BirthDay { get; set; }
    public string Password { get; set; }
    public EmployeeRole Role { get; set; }
    
    public EmployeeRequestDto()
    {
    }

    public EmployeeRequestDto(
        string cpf,
        string name,
        string surname,
        string email,
        DateOnly birthDay,
        string password,
        EmployeeRole role)
    {
        Cpf = cpf;
        Name = name;
        Surname = surname;
        Email = email;
        BirthDay = birthDay;
        Password = password;
        Role = role;
    }
}