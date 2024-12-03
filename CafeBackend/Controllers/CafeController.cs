using CafeBackend.Application.Common;
using CafeBackend.Application.Features.Cafe.Commands.CreateCafe;
using CafeBackend.Application.Features.Cafe.Commands.DeleteCafe;
using CafeBackend.Application.Features.Cafe.Commands.UpdateCafe;
using CafeBackend.Application.Features.Cafe.Queries.GetAllCafes;
using CafeBackend.Application.Features.Cafe.Queries.GetCafeDetails;
using CafeBackend.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CafeBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CafeController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CafeController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCafes([FromQuery] string? location)
        {
            var cafes = await _mediator.Send(new GetCafesQuery() { Location = location });
            return Ok(cafes);
        }
        [HttpPost]
        public async Task<IActionResult> CreateCafe([FromBody] CreateCafeCommand command)
        {
            var cafeId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetCafeById), new { id = cafeId }, null);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCafeById(Guid id)
        {
            try
            {
                var query = new GetCafeDetailsQuery(id);
                var cafe = await _mediator.Send(query);
                return Ok(cafe);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCafe(Guid id, [FromBody] UpdateCafeCommand command)
        {
            if (command == null)
            {
                return BadRequest("Request body cannot be null.");
            }

            try
            {
                // Set the Id from the route parameter
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
        public async Task<IActionResult> DeleteCafe(Guid id)
        {
            try
            {
                var command = new DeleteCafeCommand { Id = id };
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
