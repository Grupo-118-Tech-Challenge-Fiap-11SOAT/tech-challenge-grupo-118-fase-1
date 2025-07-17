using Common.Dto.Employee;
using Common.Interfaces.Employee.Gateway;

namespace TechChallengeFastFood.CleanArch.Application.UseCases.Employee;

/// <summary>
/// Caso de uso para criação de um novo funcionário.
/// </summary>
public class CreateEmployeeUseCase
{
    private readonly IEmployeeGateway _employeeGateway;

    /// <summary>
    /// Inicializa uma nova instância de <see cref="CreateEmployeeUseCase"/>.
    /// </summary>
    /// <param name="employeeGateway">Gateway para operações de funcionário.</param>
    public CreateEmployeeUseCase(IEmployeeGateway employeeGateway)
    {
        _employeeGateway = employeeGateway;
    }

    /// <summary>
    /// Cria uma instância de <see cref="CreateEmployeeUseCase"/>.
    /// </summary>
    /// <param name="employeeGateway">Gateway para operações de funcionário.</param>
    /// <returns>Instância de <see cref="CreateEmployeeUseCase"/>.</returns>
    public static CreateEmployeeUseCase Create(IEmployeeGateway employeeGateway)
    {
        return new CreateEmployeeUseCase(employeeGateway);
    }

    /// <summary>
    /// Executa o caso de uso para criar um novo funcionário.
    /// </summary>
    /// <param name="employeeRequestDto">DTO contendo os dados do funcionário a ser criado.</param>
    /// <param name="cancellationToken">Token para cancelamento da operação assíncrona.</param>
    /// <returns>Entidade <see cref="Domain.Entities.Employee.Entities.Employee"/> criada.</returns>
    public async Task<Domain.Entities.Employee.Entities.Employee> ExecuteAsync(EmployeeRequestDto employeeRequestDto, CancellationToken cancellationToken)
    {
        var employee = new Domain.Entities.Employee.Entities.Employee(
            employeeRequestDto.Cpf,
            employeeRequestDto.Name,
            employeeRequestDto.Surname,
            employeeRequestDto.Email,
            employeeRequestDto.BirthDay,
            employeeRequestDto.Password,
            employeeRequestDto.Role,
            true);

        return await _employeeGateway.CreateAsync(employee, cancellationToken);
    }
}
