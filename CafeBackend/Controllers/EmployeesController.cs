using CafeBackend.Application.Common;
using CafeBackend.Application.Features.Cafe.Commands.DeleteCafe;
using CafeBackend.Application.Features.Cafe.Queries.GetCafeDetails;
using CafeBackend.Application.Features.Employee.Commands.CreateEmployee;
using CafeBackend.Application.Features.Employee.Commands.DeleteEmployee;
using CafeBackend.Application.Features.Employee.Commands.UpdateEmployee;
using CafeBackend.Application.Features.Employee.Queries.GetAllEmployees;
using CafeBackend.Application.Features.Employee.Queries.GetEmployeeDetails;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CafeBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmployeesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployees([FromQuery] string? cafeName)
        {
            var query = new GetEmployeesQuery { CafeName = cafeName };
            var employees = await _mediator.Send(query);
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(string id)
        {
            try
            {
                var query = new GetEmployeeDetailsQuery(id);
                var employee = await _mediator.Send(query);
                return Ok(employee);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }


        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeCommand command)
        {
            try
            {
                var employeeId = await _mediator.Send(command);
                return CreatedAtAction(nameof(GetEmployeeById), new { id = employeeId }, null);
            }
            catch (BadRequestException ex)
            {
                return BadRequest(new {message = ex.Message, errors = ex.ValidationErrors});
            }

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(string id, [FromBody] UpdateEmployeeCommand command)
        {
            try
            {
                command.Id = id;
                var cafe = await _mediator.Send(command);

                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(string id)
        {
            try
            {
                var command = new DeleteEmployeeCommand { Id = id };
                await _mediator.Send(command);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }

        }
    }
}
