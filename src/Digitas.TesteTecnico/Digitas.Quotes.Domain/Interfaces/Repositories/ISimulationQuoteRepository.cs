using Digitas.Quotes.Domain.Entities;

namespace Digitas.Quotes.Domain.Interfaces.Repositories;

public interface ISimulationQuoteRepository
{
    Task AddAsync(SimulationQuote simulationQuoute);
    Task<SimulationQuote?> GetSimulationQuoteByQuoteHashIdentifier(string idSimulation);
}
