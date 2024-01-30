using Digitas.Quotes.Domain.Enums;
using Digitas.Quotes.Domain.ValueObjects;

namespace Digitas.Quotes.Domain.Interfaces.Services;

public interface ISimulationService
{
    Task<Simulation?> GetSimulationBySimulationQuoteHashIdentifier(string idSimulation);
    Task ProcessSimulationData(Simulation operationResult, Coin coin);
}
