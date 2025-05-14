using Application.Employee.Dtos;
using Application.Employee.Ports;
using Application.Employee.Request;
using Microsoft.AspNetCore.Mvc;
using static Application.Reponse;

namespace TechChallengeFastFood.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeManager _employeeManager;
        public EmployeeController(IEmployeeManager employeeManager)
        {
            _employeeManager = employeeManager;
        }

        /// <summary>
        /// Creates a new employee.
        /// </summary>
        /// <param name="employeeDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<EmployeeDto>> Post([FromBody] EmployeeDto employeeDto)
        {
            var createEmployeeRequest = new CreateEmployeeRequest
            {
                Data = employeeDto
            };

            var result = await _employeeManager.Create(createEmployeeRequest);

            if (result.Success)
            {
                return Created("", result.Data);
            }

            return result.ErrorCode switch
            {
                ErrorCode.NotFound => NotFound(result),
                ErrorCode.InvalidInput => BadRequest(result),
                _ => BadRequest(500)
            };
        }

        /// <summary>
        /// Updates an existing employee.
        /// </summary>
        /// <param name="employeeDto"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult<EmployeeDto>> Update([FromBody] EmployeeDto employeeDto)
        {
            var updateEmployeeRequest = new UpdateEmployeeRequest
            {
                Data = employeeDto
            };

            var result = await _employeeManager.Update(updateEmployeeRequest);

            if (result.Success)
            {
                return Ok(result.Data);
            }

            return result.ErrorCode switch
            {
                ErrorCode.NotFound => NotFound(result),
                ErrorCode.InvalidInput => BadRequest(result),
                _ => BadRequest(500)
            };
        }

        /// <summary>
        /// Deletes an employee by ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ActionResult<EmployeeDto>> Delete([FromQuery] int id)
        {
            var result = await _employeeManager.Delete(id);

            if (result.Success)
            {
                return Ok(result.Data);
            }

            return result.ErrorCode switch
            {
                ErrorCode.NotFound => NotFound(result),
                ErrorCode.InvalidInput => BadRequest(result),
                _ => BadRequest(500)
            };
        }

        /// <summary>
        /// Gets all employees.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<EmployeeDto>>> GetAll()
        {
            return Ok(await _employeeManager.GetAll());
        }

        /// <summary>
        /// Gets an employee by ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDto>> GetById(int id)
        {
            var result = await _employeeManager.GetById(id);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return result.ErrorCode switch
            {
                ErrorCode.NotFound => NotFound(result),
                ErrorCode.InvalidInput => BadRequest(result),
                _ => BadRequest(500)
            };
        }
    }
}
