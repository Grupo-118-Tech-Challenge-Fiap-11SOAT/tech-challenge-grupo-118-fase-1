using Application.Employee.Dtos;
using Application.Employee.Ports;
using Application.Employee.Request;
using Application.Employee.Responses;
using Domain.Employee.Ports;

namespace Application.Employee;

public class EmployeeManager : IEmployeeManager
{
    private readonly IEmployeeRepository _employeeRepository;

    public EmployeeManager(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<EmployeeResponse> Create(CreateEmployeeRequest createEmployeeRequest)
    {
        try
        {
            var employee = EmployeeDto.ToEntity(createEmployeeRequest.Data);

            await employee.Save(_employeeRepository);
            createEmployeeRequest.Data.Id = employee.Id;

            return new EmployeeResponse
            {
                Data = createEmployeeRequest.Data,
                Message = "Employee created successfully",
            };
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<EmployeeResponse> Delete(int id)
    {
        var employee = await _employeeRepository.GetById(id);

        if (employee == null)
        {
            return new EmployeeResponse
            {
                Success = false,
                Message = "Employee not found",
                ErrorCode = Reponse.ErrorCode.NotFound
            };
        }

        await _employeeRepository.Delete(employee);

        return new EmployeeResponse
        {
            Success = true,
            Message = "Employee deleted successfully",
        };
    }

    public async Task<List<EmployeeDto?>> GetAll()
    {
        var employeeList = await _employeeRepository.GetAll();
        return employeeList.ConvertAll(e => new EmployeeDto(e));
    }

    public async Task<EmployeeResponse> GetById(int id)
    {
        var employee = await _employeeRepository.GetById(id);
        if (employee == null)
        {
            return new EmployeeResponse
            {
                Success = false,
                Message = "Employee not found",
                ErrorCode = Reponse.ErrorCode.NotFound
            };
        }

        return new EmployeeResponse
        {
            Data = EmployeeDto.ToDto(employee),
            Message = "Employee found",
            Success = true
        };
    }

    public async Task<EmployeeResponse> Update(UpdateEmployeeRequest updateEmployeeRequest)
    {
        var employee = await _employeeRepository.GetById(updateEmployeeRequest.Data.Id);

        if (employee == null)
        {
            return new EmployeeResponse
            {
                Success = false,
                Message = "Employee not found",
                ErrorCode = Reponse.ErrorCode.NotFound
            };
        }

        employee = EmployeeDto.ToEntity(updateEmployeeRequest.Data);

        var updatedEmployee = await _employeeRepository.Update(employee);

        return new EmployeeResponse
        {
            Data = EmployeeDto.ToDto(updatedEmployee),
            Message = "Employee updated successfully",
            Success = true
        };
    }
}
