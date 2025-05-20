using Domain.Employee.Dtos;

namespace Domain.Employee.Ports.In;

public interface IEmployeeManager
{
    Task<EmployeeDto> CreateAsync(EmployeeDto employeeDto, CancellationToken cancellationToken);
    Task<EmployeeDto> UpdateAsync(EmployeeDto employeeDto, CancellationToken cancellationToken);
    Task<int> DeleteAsync(int id, CancellationToken cancellationToken);
    Task<EmployeeDto?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<List<EmployeeDto?>> GetAllAsync(CancellationToken cancellationToken, int skip = 0, int take = 10);
}

