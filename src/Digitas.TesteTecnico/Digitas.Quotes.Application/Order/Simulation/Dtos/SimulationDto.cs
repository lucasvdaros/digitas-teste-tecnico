using Digitas.Quotes.Domain.Enums;
using Digitas.Quotes.Domain.ValueObjects;

namespace Digitas.Quotes.Application.Order.Simulation.Dtos;

public class SimulationDto
{
    public string SimulationQuouteId { get; set; }
    public decimal RequestAmount { get; set; }
    public OperationChoice Operation { get; set; }
    public Coin Coin { get; set; }
    public decimal Result { get; set; }
    public List<Quote>? Quotes { get; set; }

    public static SimulationDto ResponseSimuation(Domain.ValueObjects.Simulation simulation)
    {
        return new SimulationDto()
        {
            Operation = simulation.Operation,
            Result = simulation.Result,
            Quotes = simulation.Quotes,
            RequestAmount = simulation.RequestAmount,
            SimulationQuouteId = simulation.hashIdentificacao,
            Coin = simulation.Coin
        };
    }
}
