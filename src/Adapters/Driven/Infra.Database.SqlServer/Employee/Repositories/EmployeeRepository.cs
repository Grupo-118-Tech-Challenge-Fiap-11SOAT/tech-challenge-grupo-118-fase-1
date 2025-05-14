using Domain.Employee.Ports;
using Microsoft.EntityFrameworkCore;

namespace Infra.Database.SqlServer.Employee.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _context;
        public EmployeeRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public async Task<Domain.Employee.Entities.Employee> Create(Domain.Employee.Entities.Employee employee)
        {
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();

            return employee;
        }

        public async Task Delete(Domain.Employee.Entities.Employee employee)
        {
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Domain.Employee.Entities.Employee?>> GetAll()
        {
            return await _context.Employees
                .ToListAsync();
        }

        public async Task<Domain.Employee.Entities.Employee?> GetById(int id)
        {
            return await _context.Employees
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Domain.Employee.Entities.Employee> Update(Domain.Employee.Entities.Employee employee)
        {
            _context.Update(employee);
            await _context.SaveChangesAsync();

            return employee;
        }
    }
}
