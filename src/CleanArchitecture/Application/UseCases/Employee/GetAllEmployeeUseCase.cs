using Common.Interfaces.Employee.Gateway;

namespace TechChallengeFastFood.CleanArch.Application.UseCases.Employee;

public class GetAllEmployeeUseCase
{
    private readonly IEmployeeGateway _employeeGateway;
    public GetAllEmployeeUseCase(IEmployeeGateway employeeGateway)
    {
        _employeeGateway = employeeGateway;
    }

    public static GetAllEmployeeUseCase Create(IEmployeeGateway employeeGateway)
    {
        return new GetAllEmployeeUseCase(employeeGateway);
    }

    public async Task<List<Domain.Entities.Employee.Entities.Employee>?> ExecuteAsync(CancellationToken cancellationToken, int skip = 0, int take = 10)
    {
        return await _employeeGateway.GetAllAsync(cancellationToken, skip, take);
    }
}
