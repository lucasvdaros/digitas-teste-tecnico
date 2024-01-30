using Digitas.Quotes.Application.Order.Simulation.Dtos;
using Digitas.Quotes.Domain.Enums;
using MediatR;

namespace Digitas.Quotes.Application.Order.Simulation.Commands;

public record SimulationCommand : IRequest<SimulationDto>
{
    public OperationChoice Operation { get; set; }
    public Coin Coin { get; set; }

    public decimal Amount { get; set; }

    public CoinOperation DefineOperation()
    {
        if (Operation.Equals(OperationChoice.Buy) && Coin.Equals(Coin.BTC)) return CoinOperation.BuyBTC;
        else if (Operation.Equals(OperationChoice.Buy) && Coin.Equals(Coin.ETH)) return CoinOperation.BuyETH;
        else if (Operation.Equals(OperationChoice.Sell) && Coin.Equals(Coin.BTC)) return CoinOperation.SellBTC;
        else return CoinOperation.SellETH;
    }
}
