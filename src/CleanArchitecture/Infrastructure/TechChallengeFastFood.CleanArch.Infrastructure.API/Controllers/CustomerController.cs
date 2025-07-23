using Common.Dto.Customers;
using Common.Interfaces.Customer.Controller;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TechChallengeFastFood.CleanArch.API.Controllers;

/// <summary>
/// Controlador responsavel pelas opera��es relacionadas ao cliente.
/// </summary>
[Authorize]
[Route("[controller]")]
public class CustomerController : ControllerBase
{
    private readonly ICustomerController _customerController;

    /// <summary>
    /// Inicializa uma nova instancia de <see cref="CustomerController"/>.
    /// </summary>
    /// <param name="customerController">Servico de controle de clientes.</param>
    public CustomerController(ICustomerController customerController)
    {
        _customerController = customerController;
    }

    private readonly ProblemDetails CUSTOMER_NOT_FOUND = new ProblemDetails
    {
        Title = "Customer not found",
        Status = StatusCodes.Status404NotFound,
        Detail = "The requested customer could not be found."
    };

    /// <summary>
    /// Obtem um cliente pelo CPF.
    /// </summary>
    /// <param name="cpf">CPF do cliente.</param>
    /// <param name="cancellationToken">Token de cancelamento.</param>
    /// <returns>Cliente encontrado ou <see cref="NotFoundResult"/> se nao existir.</returns>
    [ProducesResponseType(typeof(CustomerResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [HttpGet("cpf/{cpf}")]
    public async Task<IActionResult> GetCustomerByCpf(string cpf, CancellationToken cancellationToken)
    {
        var customers = await _customerController.GetCustomerByCpf(cpf, cancellationToken);

        return Ok(customers);
    }

    /// <summary>
    /// Obtem um cliente pelo ID.
    /// </summary>
    /// <param name="id">ID do cliente.</param>
    /// <param name="cancellationToken">Token de cancelamento.</param>
    /// <returns>Cliente encontrado ou <see cref="NotFoundResult"/> se nao existir.</returns>
    [ProducesResponseType(typeof(CustomerResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCustomerById(int id, CancellationToken cancellationToken)
    {
        var customer = await _customerController.GetCustomerById(id, cancellationToken);
        if (customer is null)
            return NotFound(CUSTOMER_NOT_FOUND);
        return Ok(customer);
    }

    /// <summary>
    /// Cria um novo cliente.
    /// </summary>
    /// <param name="customerDto">Dados do cliente a ser criado.</param>
    /// <param name="cancellationToken">Token de cancelamento.</param>
    /// <returns>Resultado da criacao do cliente.</returns>
    [ProducesResponseType(typeof(CustomerResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] CustomerRequestDto customerDto,
        CancellationToken cancellationToken)
    {
        var result = await _customerController.CreateAsync(customerDto, cancellationToken);
        return result.Error ? BadRequest(result) : CreatedAtAction("GetCustomerById", new { result.Id }, result);
    }

    /// <summary>
    /// Atualiza os dados de um cliente existente.
    /// </summary>
    /// <param name="id">ID do cliente.</param>
    /// <param name="customerDto">Dados atualizados do cliente.</param>
    /// <param name="cancellationToken">Token de cancelamento.</param>
    /// <returns>Cliente atualizado ou <see cref="NotFoundResult"/> se nao existir.</returns>
    [ProducesResponseType(typeof(CustomerResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] CustomerUpdateDto customerDto,
        CancellationToken cancellationToken)
    {
        var result = await _customerController.UpdateAsync(customerDto, cancellationToken);
        if (result is null)
            return NotFound(CUSTOMER_NOT_FOUND);

        return Ok(result);
    }
}