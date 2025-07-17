using Common.Interfaces.Employee.Gateway;

namespace TechChallengeFastFood.CleanArch.Application.UseCases.Employee;

public class DeleteEmployeeUseCase
{
    private readonly IEmployeeGateway _employeeGateway;
    public DeleteEmployeeUseCase(IEmployeeGateway employeeGateway)
    {
        _employeeGateway = employeeGateway;
    }

    public static DeleteEmployeeUseCase Create(IEmployeeGateway employeeGateway)
    {
        return new DeleteEmployeeUseCase(employeeGateway);
    }

    public async Task<bool> ExecuteAsync(int id, CancellationToken cancellationToken)
    {
        return await _employeeGateway.DeleteAsync(id, cancellationToken);
    }
}
