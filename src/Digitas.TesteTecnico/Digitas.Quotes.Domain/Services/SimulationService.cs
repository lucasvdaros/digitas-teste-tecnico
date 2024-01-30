using Digitas.Quotes.Domain.Entities;
using Digitas.Quotes.Domain.Enums;
using Digitas.Quotes.Domain.Interfaces;
using Digitas.Quotes.Domain.Interfaces.Repositories;
using Digitas.Quotes.Domain.Interfaces.Services;
using Digitas.Quotes.Domain.ValueObjects;

namespace Digitas.Quotes.Domain.Services;

public class SimulationService : ISimulationService
{
    private readonly IUnitOfWork uow;
    private readonly ISimulationQuoteRepository simulationQuoteRepository;
    private readonly ISimulationQuoteValueRepository simulationQuoteValueRepository;

    public SimulationService(IUnitOfWork uow,
                             ISimulationQuoteRepository simulationQuoteRepository,
                             ISimulationQuoteValueRepository simulationQuoteValueRepository)
    {
        this.uow = uow;
        this.simulationQuoteRepository = simulationQuoteRepository;
        this.simulationQuoteValueRepository = simulationQuoteValueRepository;
    }

    public async Task<Simulation?> GetSimulationBySimulationQuoteHashIdentifier(string idSimulation)
    {
        var simulacao = await simulationQuoteRepository.GetSimulationQuoteByQuoteHashIdentifier(idSimulation);

        if (simulacao is null)
            return null;

        var quotes = await simulationQuoteValueRepository.GetQuoutesValuesBySimulationId(simulacao.SimulationQuoteId);

        var quotesReturn = new List<Quote>();
        foreach (var quote in quotes)
        {
            var q = new Quote(quote.Amount, quote.UsdValue);
            quotesReturn.Add(q);
        }

        return new Simulation()
        {
            Coin = (Coin)simulacao.Coin,
            hashIdentificacao = simulacao.SimulationQuoteHashIdentifier,
            Operation = (OperationChoice)simulacao.OperationChoice,
            RequestAmount = simulacao.RequestAmount,
            Result = simulacao.FinalResult,
            Quotes = quotesReturn
        };
    }

    public async Task ProcessSimulationData(Simulation operationResult, Coin coin)
    {
        var simulationQuoute = new SimulationQuote(operationResult.hashIdentificacao,
                                                   (int)operationResult.Operation,
                                                   (int)coin,
                                                   operationResult.RequestAmount,
                                                   operationResult.Result);
        await simulationQuoteRepository.AddAsync(simulationQuoute);

        var values = SimulationQuoteValue.SimulationValues(operationResult.Quotes!, simulationQuoute.SimulationQuoteId);
        await simulationQuoteValueRepository.AddRangeAsync(values);

        uow.Commit();
    }
}