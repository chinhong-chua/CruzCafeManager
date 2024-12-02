using CafeBackend.Application.Features.Cafe.Commands.CreateCafe;
using CafeBackend.Application.Features.Cafe.Queries.GetAllCafes;
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
            var cafes = await _mediator.Send(new GetCafesQuery() { Location = location});
            return Ok(cafes);
        }
        [HttpPost]
        public async Task<IActionResult> CreateCafe([FromBody] CreateCafeCommand command)
        {
            var cafeId = await _mediator.Send(command);
            return Ok(cafeId);
        }
    }
}
