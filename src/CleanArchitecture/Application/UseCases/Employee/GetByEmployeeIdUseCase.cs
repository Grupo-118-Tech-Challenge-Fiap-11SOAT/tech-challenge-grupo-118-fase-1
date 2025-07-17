namespace TechChallengeFastFood.CleanArch.Application.UseCases.Employee;

public class GetByEmployeeIdUseCase
{
    private readonly Common.Interfaces.Employee.Gateway.IEmployeeGateway _employeeGateway;
    public GetByEmployeeIdUseCase(Common.Interfaces.Employee.Gateway.IEmployeeGateway employeeGateway)
    {
        _employeeGateway = employeeGateway;
    }

    public static GetByEmployeeIdUseCase Create(Common.Interfaces.Employee.Gateway.IEmployeeGateway employeeGateway)
    {
        return new GetByEmployeeIdUseCase(employeeGateway);
    }

    public async Task<Domain.Entities.Employee.Entities.Employee?> ExecuteAsync(int id, CancellationToken cancellationToken)
    {
        return await _employeeGateway.GetByIdAsync(id, cancellationToken);
    }
}
