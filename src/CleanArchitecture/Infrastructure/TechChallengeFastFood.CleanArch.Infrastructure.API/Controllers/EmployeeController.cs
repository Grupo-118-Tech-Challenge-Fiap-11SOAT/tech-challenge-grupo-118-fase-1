using Common.Dto.Employee;
using Common.Interfaces.Employee;
using Common.Interfaces.Employee.Controller;
using Common.Interfaces.Employee.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TechChallengeFastFood.CleanArch.API.Controllers;

/// <summary>
/// Controlador responsável pelas operações de funcionários.
/// </summary>
[ApiController]
[Route("[controller]")]
[Authorize]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeController _employeeController;

    /// <summary>
    /// Inicializa uma nova instância de <see cref="EmployeeController"/>.
    /// </summary>
    /// <param name="employeeController">Serviço de controle de funcionários.</param>
    public EmployeeController(IEmployeeRepository employeeRepository, IPasswordManager passwordManager)
    {
        _employeeController = Presentation.Controllers.Employee.EmployeeController.Create(employeeRepository, passwordManager);
    }

    private readonly ProblemDetails EMPLOYEE_NOT_FOUND = new ProblemDetails
    {
        Title = "Employee not found",
        Status = StatusCodes.Status404NotFound,
        Detail = "The requested employee could not be found."
    };

    /// <summary>
    /// Obtém um funcionário pelo identificador.
    /// </summary>
    /// <param name="id">Identificador do funcionário.</param>
    /// <param name="cancellationToken">Token de cancelamento.</param>
    /// <returns>Funcionário encontrado ou erro 404.</returns>
    [ProducesResponseType(typeof(EmployeeResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetEmployeeById(int id, CancellationToken cancellationToken)
    {
        var employee = await _employeeController.GetByIdAsync(id, cancellationToken);
        if (employee is null)
            return NotFound(EMPLOYEE_NOT_FOUND);

        return Ok(employee);
    }

    /// <summary>
    /// Obtém todos os funcionários com paginação.
    /// </summary>
    /// <param name="cancellationToken">Token de cancelamento.</param>
    /// <param name="skip">Quantidade de registros a pular.</param>
    /// <param name="take">Quantidade de registros a retornar.</param>
    /// <returns>Lista de funcionários ou erro 404.</returns>
    [ProducesResponseType(typeof(List<EmployeeResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [HttpGet]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken, int skip = 0, int take = 10)
    {
        var employees = await _employeeController.GetAllAsync(cancellationToken, skip, take);
        if (employees is null || employees.Count == 0)
            return NotFound(EMPLOYEE_NOT_FOUND);

        return Ok(employees);
    }

    /// <summary>
    /// Cria um novo funcionário.
    /// </summary>
    /// <param name="employeeDto">Dados do funcionário.</param>
    /// <param name="cancellationToken">Token de cancelamento.</param>
    /// <returns>Funcionário criado.</returns>
    [ProducesResponseType(typeof(EmployeeResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(EmployeeResponseDto), StatusCodes.Status400BadRequest)]
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] EmployeeRequestDto employeeDto,
        CancellationToken cancellationToken)
    {
        var result = await _employeeController.CreateAsync(employeeDto, cancellationToken);
        return CreatedAtAction("GetEmployeeById", new { result.Id }, result);
    }

    /// <summary>
    /// Atualiza os dados de um funcionário existente.
    /// </summary>
    /// <param name="id">Identificador do funcionário.</param>
    /// <param name="employeeDto">Dados atualizados do funcionário.</param>
    /// <param name="cancellationToken">Token de cancelamento.</param>
    /// <returns>Funcionário atualizado ou erro 404.</returns>
    [ProducesResponseType(typeof(EmployeeResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(EmployeeResponseDto), StatusCodes.Status400BadRequest)]
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] UpdateEmployeeDto employeeDto,
        CancellationToken cancellationToken)
    {
        var result = await _employeeController.UpdateAsync(employeeDto, cancellationToken);
        if (result is null)
            return NotFound(EMPLOYEE_NOT_FOUND);
        return Ok(result);
    }

    /// <summary>
    /// Remove um funcionário pelo identificador.
    /// </summary>
    /// <param name="id">Identificador do funcionário.</param>
    /// <param name="cancellationToken">Token de cancelamento.</param>
    /// <returns>Resposta sem conteúdo.</returns>
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        await _employeeController.DeleteAsync(id, cancellationToken);
        return NoContent();
    }
}