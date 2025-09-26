using Common.Dto.Customers;
using Common.Interfaces.Customer.Controller;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechChallengeFastFood.CleanArch.Infrastructure.Database;
using TechChallengeFastFood.CleanArch.Infrastructure.Database.Customers.Repositories;

namespace TechChallengeFastFood.CleanArch.API.Controllers;

/// <summary>
/// Controller responsible for managing customer operations.
/// </summary>
[Authorize]
[Route("[controller]")]
public class CustomerController : ControllerBase
{
    private readonly ICustomerController _customerController;

    /// <summary>
    /// Initializes a new instance of the <see cref="CustomerController"/> class.
    /// </summary>
    /// <param name="cleanArchDbContext"></param>
    public CustomerController(CleanArchDbContext cleanArchDbContext)
    {
        var customerRepository = CustomerRepository.Create(cleanArchDbContext);

        _customerController = Presentation.Controllers.Customer.CustomerController.Create(customerRepository);
    }

    private readonly ProblemDetails CUSTOMER_NOT_FOUND = new ProblemDetails
    {
        Title = "Customer not found",
        Status = StatusCodes.Status404NotFound,
        Detail = "The requested customer could not be found."
    };

    /// <summary>
    /// Get a customer by their CPF
    /// </summary>
    /// <param name="cpf">Customer's CPF.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Customer found or <see cref="NotFoundResult"/> if not exists.</returns>
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
    /// Get a specific customer by their ID.
    /// </summary>
    /// <param name="id">Customer ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Customer found or <see cref="NotFoundResult"/> if not exists.</returns>
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
}