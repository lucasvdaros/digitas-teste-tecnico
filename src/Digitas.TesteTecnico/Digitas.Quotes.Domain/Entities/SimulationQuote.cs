namespace Digitas.Quotes.Domain.Entities;

public class SimulationQuote
{
    public int SimulationQuoteId { get; set; }
    public string SimulationQuoteHashIdentifier { get; set; }
    public int OperationChoice { get; set; }
    public int Coin { get; set; }
    public decimal RequestAmount { get; set; }    
    public decimal FinalResult { get; set; }

    public virtual ICollection<SimulationQuoteValue>? SimulationQuotesValues { get; set; }

    public SimulationQuote(string simulationQuoteHashIdentifier, 
                           int operationChoice,
                           int coin,
                           decimal requestAmount,
                           decimal finalResult)
    {
        SimulationQuoteHashIdentifier = simulationQuoteHashIdentifier;
        OperationChoice = operationChoice;
        Coin = coin;
        RequestAmount = requestAmount; 
        FinalResult = finalResult;        
    }
}
