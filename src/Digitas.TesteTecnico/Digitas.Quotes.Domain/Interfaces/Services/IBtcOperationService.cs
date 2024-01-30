using Digitas.Quotes.Domain.Enums;
using Digitas.Quotes.Domain.ValueObjects;

namespace Digitas.Quotes.Domain.Interfaces.Services;

public interface IBtcOperationService
{
    public Task<Simulation> CalculateBtcBuyOperation(OperationChoice operation, decimal amount);
    Task<Simulation> CalculateBtcSellOperation(OperationChoice operation, decimal amount);
}
