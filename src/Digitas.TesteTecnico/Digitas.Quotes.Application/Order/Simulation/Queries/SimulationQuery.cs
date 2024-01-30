using Digitas.Quotes.Application.Order.Simulation.Dtos;
using MediatR;

namespace Digitas.Quotes.Application.Order.Simulation.Queries;

public record SimulationQuery : IRequest<Domain.ValueObjects.Simulation>
{
    public string IdSimulation { get; set; }
}
