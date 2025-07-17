using Common.Dto.Employee;
using Common.Interfaces.Employee.Controller;
using Common.Interfaces.Employee.Gateway;
using Common.Interfaces.Employee.Presenter;
using TechChallengeFastFood.CleanArch.Application.UseCases.Employee;
using TechChallengeFastFood.CleanArch.Application.UseCases.Products.ImageProduct;

namespace TechChallengeFastFood.CleanArch.Presentation.Controllers.Employee;

public class EmployeeController : IEmployeeController
{
    private readonly CreateEmployeeUseCase _createEmployeeUseCase;
    private readonly GetAllEmployeeUseCase _getAllEmployeeUseCase;
    private readonly GetByEmployeeIdUseCase _getEmployeeByIdUseCase;
    private readonly UpdateEmployeeUseCase _updateEmployeeUseCase;
    private readonly DeleteEmployeeUseCase _deleteEmployeeUseCase;

    private readonly IEmployeePresenter _employeePresenter;

    public EmployeeController(IEmployeeGateway employeeGateway, IEmployeePresenter employeePresenter)
    {
        _createEmployeeUseCase = CreateEmployeeUseCase.Create(employeeGateway);
        _getAllEmployeeUseCase = GetAllEmployeeUseCase.Create(employeeGateway);
        _getEmployeeByIdUseCase = GetByEmployeeIdUseCase.Create(employeeGateway);
        _updateEmployeeUseCase = UpdateEmployeeUseCase.Create(employeeGateway);
        _deleteEmployeeUseCase = DeleteEmployeeUseCase.Create(employeeGateway);

        _employeePresenter = employeePresenter;
    }

    /// <summary>
    /// Cria um novo funcionário.
    /// </summary>
    /// <param name="employee">DTO contendo os dados do funcionário a ser criado.</param>
    /// <param name="cancellationToken">Token para cancelamento da operação assíncrona.</param>
    /// <returns>
    /// <see cref="EmployeeResponseDto"/> representando o funcionário criado.
    /// </returns>
    public async Task<EmployeeResponseDto> CreateAsync(EmployeeRequestDto employee, CancellationToken cancellationToken = default)
    {
        var createdEmployee = await _createEmployeeUseCase.ExecuteAsync(employee, cancellationToken);

        return _employeePresenter.Convert(createdEmployee);
    }

    /// <summary>
    /// Exclui um funcionário pelo identificador fornecido.
    /// </summary>
    /// <param name="id">Identificador único do funcionário a ser excluído.</param>
    /// <param name="cancellationToken">Token para cancelamento da operação assíncrona.</param>
    /// <returns>
    /// <c>true</c> se o funcionário foi excluído com sucesso; caso contrário, <c>false</c>.
    /// </returns>
    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var deleteEmployeeUseCase = await _deleteEmployeeUseCase.ExecuteAsync(id, cancellationToken);

        return deleteEmployeeUseCase;
    }

    /// <summary>
    /// Recupera uma lista paginada de funcionários.
    /// </summary>
    /// <param name="cancellationToken">Token para cancelamento da operação assíncrona.</param>
    /// <param name="skip">Quantidade de registros a serem ignorados para paginação.</param>
    /// <param name="take">Quantidade máxima de registros a serem retornados.</param>
    /// <returns>
    /// Uma lista de <see cref="EmployeeResponseDto"/> representando os funcionários encontrados, ou <c>null</c> se nenhum funcionário for encontrado.
    /// </returns>
    public async Task<List<EmployeeResponseDto>?> GetAllAsync(CancellationToken cancellationToken = default, int skip = 0, int take = 10)
    {
        var employees = await _getAllEmployeeUseCase.ExecuteAsync(cancellationToken, skip, take);

        return _employeePresenter.Convert(employees);
    }

    /// <summary>
    /// Recupera um funcionário pelo identificador fornecido.
    /// </summary>
    /// <param name="id">Identificador único do funcionário.</param>
    /// <param name="cancellationToken">Token para cancelamento da operação assíncrona.</param>
    /// <returns>
    /// <see cref="EmployeeResponseDto"/> representando o funcionário encontrado, ou <c>null</c> se não existir.
    /// </returns>
    public async Task<EmployeeResponseDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var employee = await _getEmployeeByIdUseCase.ExecuteAsync(id, cancellationToken);

        return _employeePresenter.Convert(employee);
    }

    /// <summary>
    /// Atualiza os dados de um funcionário existente.
    /// </summary>
    /// <param name="employee">DTO contendo os dados atualizados do funcionário.</param>
    /// <param name="cancellationToken">Token para cancelamento da operação assíncrona.</param>
    /// <returns>
    /// <see cref="EmployeeResponseDto"/> representando o funcionário atualizado, ou <c>null</c> se o funcionário não existir.
    /// </returns>
    public async Task<EmployeeResponseDto?> UpdateAsync(UpdateEmployeeDto employee, CancellationToken cancellationToken = default)
    {
        var existingEmployee = await _getEmployeeByIdUseCase.ExecuteAsync(employee.Id, cancellationToken);
        if (existingEmployee is null)
        {
            return null;
        }

        var updateEmployeeUseCase = await _updateEmployeeUseCase.ExecuteAsync(employee, existingEmployee, cancellationToken);

        return _employeePresenter.Convert(updateEmployeeUseCase);
    }
}
