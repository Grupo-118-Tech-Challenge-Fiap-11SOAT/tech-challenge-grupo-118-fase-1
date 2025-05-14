using Application.Employee.Dtos;
using Application.Employee.Request;
using Application.Employee.Responses;

namespace Application.Employee.Ports;

public interface IEmployeeManager
{
    Task<EmployeeResponse> Create(CreateEmployeeRequest createEmployeeRequest);
    Task<EmployeeResponse> Update(UpdateEmployeeRequest updateEmployeeRequest);
    Task<EmployeeResponse> Delete(int id);
    Task<EmployeeResponse?> GetById(int id);
    Task<List<EmployeeDto?>> GetAll();
}

