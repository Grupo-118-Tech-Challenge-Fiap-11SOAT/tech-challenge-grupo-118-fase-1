using Common.Dto.Employee;
using Common.Interfaces.Employee.Presenter;

namespace TechChallengeFastFood.CleanArch.Presentation.Presenters.Employee;

public class EmployeePresenter : IEmployeePresenter
{
    public static IEmployeePresenter Create()
    {
        return new EmployeePresenter();
    }

    public List<EmployeeResponseDto> Convert(List<Domain.Entities.Employee.Entities.Employee> employees)
    {
        if (employees is null || employees.Count == 0)
            return null;

        var employeeResponseDtos = new List<EmployeeResponseDto>();

        employees.ToList().ForEach(employee =>
        {
            var employeeResponseDto = new EmployeeResponseDto
            {
                Id = employee.Id,
                Cpf = employee.Cpf,
                Name = employee.Name,
                Surname = employee.Surname,
                Email = employee.Email,
                BirthDate = employee.BirthDay,
                Role = employee.Role,
                IsActive = employee.IsActive
            };
            employeeResponseDtos.Add(employeeResponseDto);
        });

        return employeeResponseDtos;
    }

    public EmployeeResponseDto Convert(Domain.Entities.Employee.Entities.Employee employee)
    {
        if (employee is null)
            return null;

        return new EmployeeResponseDto
        {
            Id = employee.Id,
            Cpf = employee.Cpf,
            Name = employee.Name,
            Surname = employee.Surname,
            Email = employee.Email,
            BirthDate = employee.BirthDay,
            Role = employee.Role,
            IsActive = employee.IsActive
        };
    }
}