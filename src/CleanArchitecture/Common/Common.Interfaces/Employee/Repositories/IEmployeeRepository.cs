namespace Common.Interfaces.Employee.Repositories;

public interface IEmployeeRepository
{
    Task<List<Dto.Employee.Database.Employee>> GetAllAsync(CancellationToken cancellationToken = default, int skip = 0, int take = 10);
    Task<Dto.Employee.Database.Employee> CreateAsync(Dto.Employee.Database.Employee employee, CancellationToken cancellationToken = default);
    Task<Dto.Employee.Database.Employee?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task UpdateAsync(Dto.Employee.Database.Employee employee, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}
