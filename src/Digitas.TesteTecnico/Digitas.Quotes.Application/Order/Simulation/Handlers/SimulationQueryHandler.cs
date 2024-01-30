using Digitas.Quotes.Application.Order.Simulation.Queries;
using Digitas.Quotes.Domain.Interfaces.Services;
using MediatR;

namespace Digitas.Quotes.Application.Order.Simulation.Handlers;

public class SimulationQueryHandler : IRequestHandler<SimulationQuery, Domain.ValueObjects.Simulation?>
{
    private readonly ISimulationService simulationService;

    public SimulationQueryHandler(ISimulationService simulationService)
    {
        this.simulationService = simulationService;
    }

    public async Task<Domain.ValueObjects.Simulation?> Handle(SimulationQuery request, CancellationToken cancellationToken)
    {       
        return await simulationService.GetSimulationBySimulationQuoteHashIdentifier(request.IdSimulation);        
    }
}
