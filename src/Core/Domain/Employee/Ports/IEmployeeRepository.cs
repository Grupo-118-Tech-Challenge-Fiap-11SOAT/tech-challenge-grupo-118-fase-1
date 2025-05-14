namespace Domain.Employee.Ports
{
    public interface IEmployeeRepository
    {
        Task<Entities.Employee> Create(Entities.Employee employee);
        Task<Entities.Employee> Update(Entities.Employee employee);
        Task Delete(Entities.Employee employee);
        Task<Entities.Employee?> GetById(int id);
        Task<List<Entities.Employee?>> GetAll();
    }
}
