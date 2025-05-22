using Domain.Customer.Dtos;
using Domain.Customer.Ports.In;
using Domain.Employee.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace TechChallengeFastFood.API.Controllers
{
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerManager _customerManager;

        public CustomerController(ICustomerManager customerManager)
        {
            _customerManager = customerManager;
        }

        [HttpPut]
        public async Task<ActionResult<CustomerDto>> PutAsync([FromBody] CustomerDto customerDto, CancellationToken cancellationToken)
        {
            var result = await _customerManager.UpdateAsync(customerDto, cancellationToken);

            return result.Error ? BadRequest(result) : Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<CustomerDto>> PostAsync([FromBody] CustomerDto customerDto, CancellationToken cancellationToken)
        {
            var result = await _customerManager.CreateAsync(customerDto, cancellationToken);

            return result.Error ? BadRequest(result) : Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDto>> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var employee = await _customerManager.GetByIdAsync(id, cancellationToken);

            return employee is null ? NotFound() : Ok(employee);
        }

    }
}