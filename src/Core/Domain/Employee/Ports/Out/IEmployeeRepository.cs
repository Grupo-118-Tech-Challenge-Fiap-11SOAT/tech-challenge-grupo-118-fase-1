namespace Domain.Employee.Ports.Out;

public interface IEmployeeRepository
{
    Task<Entities.Employee> Create(Entities.Employee employee);
    Task<Entities.Employee> Update(Entities.Employee employee);
    Task<int> Delete(Entities.Employee employee);
    Task<Entities.Employee?> GetById(int id);
    Task<List<Entities.Employee?>> GetAll();
}