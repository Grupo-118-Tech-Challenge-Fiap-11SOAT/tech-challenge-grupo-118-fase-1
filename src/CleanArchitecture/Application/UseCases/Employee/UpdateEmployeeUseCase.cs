using Common.Dto.Employee;
using Common.Interfaces.Employee.Gateway;

namespace TechChallengeFastFood.CleanArch.Application.UseCases.Employee;

public class UpdateEmployeeUseCase
{
    private readonly IEmployeeGateway _employeeGateway;
    private readonly GetByEmployeeIdUseCase _getByEmployeeIdUseCase;

    public UpdateEmployeeUseCase(IEmployeeGateway employeeGateway)
    {
        _employeeGateway = employeeGateway;
        _getByEmployeeIdUseCase= GetByEmployeeIdUseCase.Create(employeeGateway);
    }

    public static UpdateEmployeeUseCase Create(IEmployeeGateway employeeGateway)
    {
        return new UpdateEmployeeUseCase(employeeGateway);
    }

    public async Task<Domain.Entities.Employee.Entities.Employee?> ExecuteAsync(UpdateEmployeeDto updateEmployeeDto, Domain.Entities.Employee.Entities.Employee employee, CancellationToken cancellationToken)
    {
        employee.UpdateEmployee(updateEmployeeDto.Cpf,
            updateEmployeeDto.Name,
            updateEmployeeDto.Surname,
            updateEmployeeDto.Email,
            updateEmployeeDto.BirthDate,
            updateEmployeeDto.Password,
            updateEmployeeDto.Role,
            updateEmployeeDto.IsActive);

        return await _employeeGateway.UpdateAsync(employee, cancellationToken);
    }
}
