using Domain.Customer.Dtos;
using Domain.Customer.Ports.In;
using Microsoft.AspNetCore.Mvc;

namespace TechChallengeFastFood.API.Controllers
{
    /// <summary>
    /// Endpoint to Manage Customers
    /// </summary>
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerManager _customerManager;

        /// <summary>
        /// Customer constructor
        /// </summary>
        /// <param name="customerManager"></param>
        public CustomerController(ICustomerManager customerManager) => _customerManager = customerManager;

        /// <summary>
        /// Update a Customer
        /// </summary>
        /// <param name="customerDto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(CustomerDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails),StatusCodes.Status400BadRequest)]
        [HttpPut]
        public async Task<ActionResult<CustomerDto>> PutAsync([FromBody] CustomerDto customerDto, CancellationToken cancellationToken)
        {
            var result = await _customerManager.UpdateAsync(customerDto, cancellationToken);

            return result.Error ? BadRequest(result) : Ok(result);
        }
        /// <summary>
        /// Create a new Customer
        /// </summary>
        /// <param name="customerDto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(CustomerDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails),StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<ActionResult<CustomerDto>> PostAsync([FromBody] CustomerDto customerDto, CancellationToken cancellationToken)
        {
            var result = await _customerManager.CreateAsync(customerDto, cancellationToken);

            return result.Error ? BadRequest(result) : Ok(result);
        }
        
        /// <summary>
        /// Return a Customer filtering by Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(CustomerDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails),StatusCodes.Status400BadRequest)]
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDto>> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var customer = await _customerManager.GetByIdAsync(id, cancellationToken);

            return customer is null ? NotFound() : Ok(customer);
        }
        
        /// <summary>
        /// Return a Customer filtering by CPF
        /// </summary>
        /// <param name="cpf"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(CustomerDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails),StatusCodes.Status400BadRequest)]
        [HttpGet("{cpf}")]
        public async Task<ActionResult<CustomerDto>> GetByIdAsync(string cpf, CancellationToken cancellationToken)
        {
            var customer = await _customerManager.GetByCpfAsync(cpf, cancellationToken);

            return customer is null ? NotFound() : Ok(customer);
        }
    }
}