using Common.Interfaces.Employee.Repositories;
using Microsoft.EntityFrameworkCore;

namespace TechChallengeFastFood.CleanArch.Infrastructure.Database.Employee.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly CleanArchDbContext _dbContext;

    public EmployeeRepository(CleanArchDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Common.Dto.Employee.Database.Employee> CreateAsync(Common.Dto.Employee.Database.Employee employee, CancellationToken cancellationToken = default)
    {
        await _dbContext.Employees.AddAsync(employee, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return employee;
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        _dbContext.Employees.Remove(new Common.Dto.Employee.Database.Employee(id));
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<Common.Dto.Employee.Database.Employee>> GetAllAsync(CancellationToken cancellationToken = default, int skip = 0, int take = 10)
    {
        return await _dbContext.Employees
            .Skip(skip)
            .Take(take)
            .ToListAsync(cancellationToken);
    }

    public async Task<Common.Dto.Employee.Database.Employee?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Employees
            .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
    }

    public async Task UpdateAsync(Common.Dto.Employee.Database.Employee employee, CancellationToken cancellationToken = default)
    {
        _dbContext.Employees.Update(employee).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
