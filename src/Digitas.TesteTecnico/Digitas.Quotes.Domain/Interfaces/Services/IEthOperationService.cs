using Digitas.Quotes.Domain.Enums;
using Digitas.Quotes.Domain.ValueObjects;

namespace Digitas.Quotes.Domain.Interfaces.Services;

public interface IEthOperationService
{
    Task<Simulation> CalculateEthBuyOperation(OperationChoice operation, decimal amount);
    Task<Simulation> CalculateEthSellOperation(OperationChoice operation, decimal amount);
}
