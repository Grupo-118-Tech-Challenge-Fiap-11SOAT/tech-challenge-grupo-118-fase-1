using Domain.Employee.Ports.Out;
using Microsoft.EntityFrameworkCore;

namespace Infra.Database.SqlServer.Employee.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly AppDbContext _context;
    public EmployeeRepository(AppDbContext appDbContext)
    {
        _context = appDbContext;
    }

    /// <summary>
    /// Asynchronously creates a new employee record in the database.
    /// </summary>
    /// <param name="employee">The employee entity to be added to the database.</param>
    /// <returns>The created employee entity.</returns>
    /// <exception cref="DbUpdateException">Thrown if an error occurs while saving changes to the database.</exception>
    public async Task<Domain.Employee.Entities.Employee> Create(Domain.Employee.Entities.Employee employee)
    {
        try
        {
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();

            return employee;
        }
        catch (Exception ex)
        {

            throw;
        }
    }


    /// <summary>
    /// Asynchronously deletes an employee record from the database.
    /// </summary>
    /// <param name="employee">The employee entity to be removed from the database.</param>
    /// <returns>The number of state entries written to the database.</returns>
    /// <exception cref="DbUpdateException">Thrown if an error occurs while saving changes to the database.</exception>
    public async Task<int> Delete(Domain.Employee.Entities.Employee employee)
    {
        _context.Employees.Remove(employee);
        return await _context.SaveChangesAsync();
    }

    /// <summary>  
    /// Asynchronously retrieves all employee records from the database.  
    /// </summary>  
    /// <returns>A list of all employee entities, or an empty list if no employees are found.</returns>  
    public async Task<List<Domain.Employee.Entities.Employee?>> GetAll()
    {
        return await _context.Employees
            .ToListAsync(); ;
    }

    /// <summary>  
    /// Asynchronously retrieves an employee record from the database by its unique identifier.  
    /// </summary>  
    /// <param name="id">The unique identifier of the employee to retrieve.</param>  
    /// <returns>The employee entity if found; otherwise, null.</returns>  
    public async Task<Domain.Employee.Entities.Employee?> GetById(int id)
    {
        return await _context.Employees
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    /// <summary>  
    /// Asynchronously updates an existing employee record in the database.  
    /// </summary>  
    /// <param name="employee">The employee entity with updated information to be saved to the database.</param>  
    /// <returns>The updated employee entity.</returns>  
    /// <exception cref="DbUpdateException">Thrown if an error occurs while saving changes to the database.</exception>  
    public async Task<Domain.Employee.Entities.Employee> Update(Domain.Employee.Entities.Employee employee)
    {
        _context.Update(employee);
        await _context.SaveChangesAsync();

        return employee;
    }
}
