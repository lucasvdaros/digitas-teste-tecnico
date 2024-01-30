using Digitas.Quotes.Application.Order.Simulation.Commands;
using Digitas.Quotes.Application.Order.Simulation.Dtos;
using Digitas.Quotes.Application.Order.Simulation.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Digitas.Quotes.Api.Controllers;

public class OrderController : ApiControllerBase
{
    [HttpPost("simulate")]
    [AllowAnonymous]
    public async Task<ActionResult<SimulationDto>> CalculeSimulation([FromBody] SimulationCommand command)
    {
        var result = await Mediator.Send(command);

        return Created(result.SimulationQuouteId, result);
    }

    [HttpGet("memory")]
    [AllowAnonymous]
    public async Task<ActionResult<SimulationDto>> GetMemoryCalculation([FromQuery] SimulationQuery query)
    {
        var result = await Mediator.Send(query);

        if (result is null) 
            return NoContent();

        return Ok(result);
    }
}
