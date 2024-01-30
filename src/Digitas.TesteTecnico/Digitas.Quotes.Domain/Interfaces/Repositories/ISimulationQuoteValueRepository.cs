using Digitas.Quotes.Domain.Entities;

namespace Digitas.Quotes.Domain.Interfaces.Repositories;

public interface ISimulationQuoteValueRepository
{
    Task AddRangeAsync(IEnumerable<SimulationQuoteValue> simulationQuoute);
    Task<List<SimulationQuoteValue>> GetQuoutesValuesBySimulationId(int simulationQuoteId);
}
