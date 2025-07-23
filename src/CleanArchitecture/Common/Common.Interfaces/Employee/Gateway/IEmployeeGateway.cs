using EmployeeDomain = TechChallengeFastFood.CleanArch.Domain.Entities.Employee.Entities.Employee;

namespace Common.Interfaces.Employee.Gateway;

public interface IEmployeeGateway
{
    Task<List<EmployeeDomain>?> GetAllAsync(CancellationToken cancellationToken = default, int skip = 0, int take = 10);
    Task<EmployeeDomain> CreateAsync(EmployeeDomain employee, CancellationToken cancellationToken = default);
    Task<EmployeeDomain?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<EmployeeDomain?> UpdateAsync(EmployeeDomain employee, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<EmployeeDomain?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
}
