namespace Domain.Employee.Ports
{
    public interface IEmployeeRepository
    {
        Task<Entities.Employee> Create(Entities.Employee employee);
        Task<Entities.Employee> Update(Entities.Employee employee);
        Task<bool> Delete(int id);
        Task<Entities.Employee?> GetById(Guid id);
        Task<Entities.Employee?> GetAll();
    }
}
