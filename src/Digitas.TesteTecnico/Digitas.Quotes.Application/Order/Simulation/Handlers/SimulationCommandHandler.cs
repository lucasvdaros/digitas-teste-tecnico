using Digitas.Quotes.Application.Order.Simulation.Commands;
using Digitas.Quotes.Application.Order.Simulation.Dtos;
using Digitas.Quotes.Domain.Enums;
using MediatR;
using Digitas.Quotes.Domain.Interfaces.Services;

namespace Digitas.Quotes.Application.Order.Simulation.Handlers;

public class SimulationCommandHandler : IRequestHandler<SimulationCommand, SimulationDto>
{
    private readonly IBtcOperationService btcOperationService;
    private readonly IEthOperationService ethOperationService;
    private readonly ISimulationService simulationService;

    public SimulationCommandHandler(IBtcOperationService btcOperationService,
                                  IEthOperationService ethOperationService,
                                  ISimulationService simulationService)
    {   
        this.btcOperationService = btcOperationService;
        this.ethOperationService = ethOperationService;
        this.simulationService = simulationService;
    }

    public async Task<SimulationDto> Handle(SimulationCommand request, CancellationToken cancellationToken)
    {
        Domain.ValueObjects.Simulation operationResult;
        
        var operation = request.DefineOperation();

        if (operation.Equals(CoinOperation.BuyBTC))
        {
            operationResult = await btcOperationService.CalculateBtcBuyOperation(request.Operation, request.Amount);                       
        }
        else if (operation.Equals(CoinOperation.BuyETH))
        {
            operationResult = await ethOperationService.CalculateEthBuyOperation(request.Operation, request.Amount);            
        }
        else if (operation.Equals(CoinOperation.SellBTC))
        {
            operationResult = await btcOperationService.CalculateBtcSellOperation(request.Operation, request.Amount);
        }
        else // CoinOperation.SellETH
        {
            operationResult = await ethOperationService.CalculateEthSellOperation(request.Operation, request.Amount);
        }        

        await simulationService.ProcessSimulationData(operationResult, request.Coin);

        return SimulationDto.ResponseSimuation(operationResult);
    }
}
