using Domain.Employee.Dtos;

namespace Domain.Employee.Ports;

public interface IEmployeeManager
{
    Task<EmployeeDto> Create(EmployeeDto employeeDto);
    Task<EmployeeDto> Update(EmployeeDto employeeDto);
    Task<int> Delete(int id);
    Task<EmployeeDto?> GetById(int id);
    Task<List<EmployeeDto?>> GetAll();
}

