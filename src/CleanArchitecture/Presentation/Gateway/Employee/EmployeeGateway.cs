using Common.Interfaces.Employee;
using Common.Interfaces.Employee.Gateway;
using Common.Interfaces.Employee.Repositories;
using System.Globalization;
using EmployeeDomain = TechChallengeFastFood.CleanArch.Domain.Entities.Employee.Entities.Employee;
using EmployeeEntity = Common.Dto.Employee.Database.Employee;

namespace TechChallengeFastFood.CleanArch.Presentation.Gateway.Employee;

public class EmployeeGateway : IEmployeeGateway
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IPasswordManager _passwordManager;

    public EmployeeGateway(IEmployeeRepository employeeRepository, IPasswordManager passwordManager)
    {
        _employeeRepository = employeeRepository;
        _passwordManager = passwordManager;
    }

    public static IEmployeeGateway Create(IEmployeeRepository employeeRepository, IPasswordManager passwordManager)
    {
        return new EmployeeGateway(employeeRepository, passwordManager);
    }

    /// <summary>
    /// Cria um novo funcionário no repositório.
    /// </summary>
    /// <param name="employee">Objeto de domínio do funcionário a ser criado.</param>
    /// <param name="cancellationToken">Token para cancelamento da operação assíncrona.</param>
    /// <returns>Retorna o funcionário criado como objeto de domínio.</returns>
    public async Task<EmployeeDomain> CreateAsync(EmployeeDomain employee, CancellationToken cancellationToken = default)
    {
        _passwordManager.CreatePasswordHash(employee.Password, out var hashedPassword);
        var employeeEntity = new EmployeeEntity
        (
            employee.Cpf,
            employee.Name,
            employee.Surname,
            employee.Email,
            employee.BirthDay,
            hashedPassword,
            employee.Role,
            employee.IsActive
        );

        var createdEmployee = await _employeeRepository.CreateAsync(employeeEntity, cancellationToken);

        return new EmployeeDomain
        (
            createdEmployee.Cpf,
            createdEmployee.Name,
            createdEmployee.Surname,
            createdEmployee.Email,
            createdEmployee.BirthDay,
            createdEmployee.Password,
            createdEmployee.Role,
            createdEmployee.IsActive,
            createdEmployee.Id
        );
    }

    /// <summary>
    /// Exclui um funcionário do repositório pelo identificador.
    /// </summary>
    /// <param name="id">Identificador único do funcionário a ser excluído.</param>
    /// <param name="cancellationToken">Token para cancelamento da operação assíncrona.</param>
    /// <returns>Retorna <c>true</c> se a exclusão foi realizada com sucesso.</returns>
    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        await _employeeRepository.DeleteAsync(id, cancellationToken);
        return true;
    }

    /// <summary>
    /// Recupera uma lista paginada de funcionários do repositório.
    /// </summary>
    /// <param name="cancellationToken">Token para cancelamento da operação assíncrona.</param>
    /// <param name="skip">Número de registros a serem ignorados para paginação.</param>
    /// <param name="take">Número máximo de registros a serem retornados.</param>
    /// <returns>
    /// Retorna uma lista de objetos de domínio <see cref="EmployeeDomain"/> representando os funcionários,
    /// ou <c>null</c> caso não existam registros.
    /// </returns>
    public async Task<List<EmployeeDomain>?> GetAllAsync(CancellationToken cancellationToken = default, int skip = 0, int take = 10)
    {
        var employees = await _employeeRepository.GetAllAsync(cancellationToken, skip, take);

        if (employees is null)
            return null;

        var employeeDtos = new List<EmployeeDomain>();
        employees.ForEach(employeeEntity =>
        {
            employeeDtos.Add(new EmployeeDomain
            (
                employeeEntity.Cpf,
                employeeEntity.Name,
                employeeEntity.Surname,
                employeeEntity.Email,
                employeeEntity.BirthDay,
                employeeEntity.Password,
                employeeEntity.Role,
                employeeEntity.IsActive,
                employeeEntity.Id
            ));
        });

        return employeeDtos;
    }

    public async Task<EmployeeDomain?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        var employee = await _employeeRepository.GetByEmailAsync(email, cancellationToken);

        if (employee is null)
            return null;

        return new EmployeeDomain
        (
            employee.Cpf,
            employee.Name,
            employee.Surname,
            employee.Email,
            employee.BirthDay,
            employee.Password,
            employee.Role,
            employee.IsActive,
            employee.Id
        );
    }

    /// <summary>
    /// Recupera um funcionário do repositório pelo identificador único.
    /// </summary>
    /// <param name="id">Identificador único do funcionário.</param>
    /// <param name="cancellationToken">Token para cancelamento da operação assíncrona.</param>
    /// <returns>
    /// Retorna o objeto de domínio <see cref="EmployeeDomain"/> representando o funcionário encontrado,
    /// ou <c>null</c> caso não exista um funcionário com o identificador informado.
    /// </returns>
    public async Task<EmployeeDomain?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var employee = await _employeeRepository.GetByIdAsync(id, cancellationToken);

        if (employee is null)
            return null;

        return new EmployeeDomain
        (
            employee.Cpf,
            employee.Name,
            employee.Surname,
            employee.Email,
            employee.BirthDay,
            employee.Password,
            employee.Role,
            employee.IsActive,
            employee.Id
        );
    }

    /// <summary>
    /// Atualiza os dados de um funcionário existente no repositório.
    /// </summary>
    /// <param name="employee">Objeto de domínio do funcionário com os dados atualizados.</param>
    /// <param name="cancellationToken">Token para cancelamento da operação assíncrona.</param>
    /// <returns>
    /// Retorna o objeto de domínio <see cref="EmployeeDomain"/> representando o funcionário atualizado,
    /// ou <c>null</c> caso a atualização não seja bem-sucedida.
    /// </returns>
    public async Task<EmployeeDomain?> UpdateAsync(EmployeeDomain employee, CancellationToken cancellationToken = default)
    {
        var employeeEntity = new EmployeeEntity
        (
            employee.Cpf,
            employee.Name,
            employee.Surname,
            employee.Email,
            employee.BirthDay,
            employee.Password,
            employee.Role,
            employee.IsActive,
            employee.Id
        );

        await _employeeRepository.UpdateAsync(employeeEntity, cancellationToken);

        return employee;
    }
}
