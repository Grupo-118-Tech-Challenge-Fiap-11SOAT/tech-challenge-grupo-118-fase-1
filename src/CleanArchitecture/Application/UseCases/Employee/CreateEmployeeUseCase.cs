using Common.Dto.Employee;
using Common.Interfaces.Employee.Gateway;

namespace TechChallengeFastFood.CleanArch.Application.UseCases.Employee;

public class CreateEmployeeUseCase
{
    private readonly IEmployeeGateway _employeeGateway;
    public CreateEmployeeUseCase(IEmployeeGateway employeeGateway)
    {
        _employeeGateway = employeeGateway;
    }

    public static CreateEmployeeUseCase Create(IEmployeeGateway employeeGateway)
    {
        return new CreateEmployeeUseCase(employeeGateway);
    }

    public async Task<Domain.Entities.Employee.Entities.Employee> ExecuteAsync(EmployeeRequestDto employeeRequestDto, CancellationToken cancellationToken)
    {
        var employee = new Domain.Entities.Employee.Entities.Employee(
            employeeRequestDto.Cpf,
            employeeRequestDto.Name,
            employeeRequestDto.Surname,
            employeeRequestDto.Email,
            employeeRequestDto.BirthDay,
            employeeRequestDto.Password,
            employeeRequestDto.Role,
            true);

        return await _employeeGateway.CreateAsync(employee, cancellationToken);
    }
}
