using Domain.Employee.Ports;

namespace Infra.Database.SqlServer.Employee.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        public Task<Domain.Employee.Entities.Employee> Create(Domain.Employee.Entities.Employee employee)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Domain.Employee.Entities.Employee?> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Domain.Employee.Entities.Employee?> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Domain.Employee.Entities.Employee> Update(Domain.Employee.Entities.Employee employee)
        {
            throw new NotImplementedException();
        }
    }
}
