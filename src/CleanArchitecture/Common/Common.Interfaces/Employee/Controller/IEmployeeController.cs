using Common.Dto.Employee;

namespace Common.Interfaces.Employee.Controller;

public interface IEmployeeController
{
    Task<List<EmployeeResponseDto>?> GetAllAsync(CancellationToken cancellationToken = default, int skip = 0, int take = 10);
    Task<EmployeeResponseDto> CreateAsync(EmployeeRequestDto employee, CancellationToken cancellationToken = default);
    Task<EmployeeResponseDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<EmployeeResponseDto?> UpdateAsync(UpdateEmployeeDto employee, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
