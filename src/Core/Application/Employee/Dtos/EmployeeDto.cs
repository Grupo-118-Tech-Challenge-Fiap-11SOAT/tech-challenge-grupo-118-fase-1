using Application.Base.Dtos;
using Domain.Employee.Entities;
using Domain.Employee.ValueObjects;

namespace Application.Employee.Dtos
{
    public class EmployeeDto : PersonDto
    {
        public EmployeeDto()
        {
            
        }

        public EmployeeDto(Domain.Employee.Entities.Employee employee)
        {
            Id = employee.Id;
            CreatedAt = employee.CreatedAt;
            UpdatedAt = employee.UpdatedAt;
            IsActive = employee.IsActive;
            Cpf = employee.Cpf;
            Name = employee.Name;
            Surname = employee.Surname;
            Email = employee.Email;
            BirthDay = employee.BirthDay;
            Password = employee.Password;
            Role = employee.Role;
        }

        public string Password { get; set; }
        public EmployeeRole Role { get; set; }

        public static EmployeeDto ToDto(Domain.Employee.Entities.Employee employee)
        {
            return new EmployeeDto
            {
                Id = employee.Id,
                CreatedAt = employee.CreatedAt,
                UpdatedAt = employee.UpdatedAt,
                IsActive = employee.IsActive,
                Cpf = employee.Cpf,
                Name = employee.Name,
                Surname = employee.Surname,
                Email = employee.Email,
                BirthDay = employee.BirthDay,
                Password = employee.Password,
                Role = employee.Role
            };
        }

        public static Domain.Employee.Entities.Employee ToEntity(EmployeeDto employeeDto)
        {
            return new Domain.Employee.Entities.Employee
            {
                Id = employeeDto.Id,
                CreatedAt = employeeDto.CreatedAt,
                UpdatedAt = employeeDto.UpdatedAt,
                IsActive = employeeDto.IsActive,
                Cpf = employeeDto.Cpf,
                Name = employeeDto.Name,
                Surname = employeeDto.Surname,
                Email = employeeDto.Email,
                BirthDay = employeeDto.BirthDay,
                Password = employeeDto.Password,
                Role = employeeDto.Role
            };
        }
    }
}
