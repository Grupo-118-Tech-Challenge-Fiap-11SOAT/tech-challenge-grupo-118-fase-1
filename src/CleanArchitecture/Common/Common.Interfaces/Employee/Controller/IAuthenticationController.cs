using Common.Dto.Employee;
using Common.Dto.Login;

namespace Common.Interfaces.Employee.Controller;

public interface IAuthenticationController
{
    Task<EmployeeResponseDto?> RegisterAsync(EmployeeRequestDto employeeRequestDto, CancellationToken cancellationToken = default);
    
    Task<string> LoginAsync(LoginRequestDto loginRequestDto, CancellationToken cancellationToken = default);
}