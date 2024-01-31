using Digitas.Quotes.Worker.Domain.Entities;
using Digitas.Quotes.Worker.Domain.Interfaces;
using Digitas.Quotes.Worker.Domain.Interfaces.Repositories;
using Digitas.Quotes.Worker.Domain.Interfaces.Services;
using Digitas.Quotes.Worker.Infra.ExternalServices.BitStamp.Interface;
using Digitas.Quotes.Worker.Infra.ExternalServices.BitStamp.Responses;
using System.Globalization;
using System.Text.Json;

namespace Digitas.Quotes.Worker.Domain.Services;

public class EthService : IEthService
{
    private readonly IBitStampService bitStamp;
    private readonly IEthAskRepository ethAskRepository;
    private readonly IEthBidRepository ethBidRepository;
    private readonly IUnitOfWork uow;

    public EthService(IBitStampService bitStamp, IEthAskRepository ethAskRepository, IEthBidRepository ethBidRepository, IUnitOfWork uow)
    {
        this.bitStamp = bitStamp;
        this.ethAskRepository = ethAskRepository;
        this.ethBidRepository = ethBidRepository;
        this.uow = uow;
    }

    public async Task PersistCurrentEthBookOffers(string channel)
    {
        while (bitStamp.IsConnectionStillOpen())
        {
            var receivedMessage = await bitStamp.ReceivedMessage();

            if (!string.IsNullOrEmpty(receivedMessage))
            {
                var newUpdatesData = JsonSerializer.Deserialize<BookOrderResponse>(receivedMessage);

                if (newUpdatesData is not null && newUpdatesData.Channel.Equals(channel))
                {
                    var newAsks = ConvertAsksToPersist(newUpdatesData.Data!);
                    var newBids = ConvertBidsToPersist(newUpdatesData.Data!);

                    Console.WriteLine($"Registering new ETH {newAsks.Count} Asks");
                    Console.WriteLine($"Registering new ETH {newBids.Count} Bids");

                    await ethAskRepository.AddRange(newAsks);
                    await ethBidRepository.AddRange(newBids);

                    uow.Commit();
                }
            }
        }
    }

    private static List<EthAsk> ConvertAsksToPersist(BookOrderDataResponse data)
    {
        var EthAsks = new List<EthAsk>();

        foreach (var ask in data.Asks)
        {
            var newAsk = new EthAsk()
            {
                Microtimestamp = Convert.ToInt64(data.Microtimestamp),
                UsdValue = Convert.ToDecimal(ask[0]),
                Amount = Convert.ToDecimal(ask[1], CultureInfo.InvariantCulture)
            };

            EthAsks.Add(newAsk);
        }

        return EthAsks;
    }

    private static List<EthBid> ConvertBidsToPersist(BookOrderDataResponse data)
    {
        var EthBids = new List<EthBid>();

        foreach (var bid in data.Bids)
        {
            var newBid = new EthBid()
            {
                Microtimestamp = Convert.ToInt64(data.Microtimestamp),
                UsdValue = Convert.ToDecimal(bid[0]),
                Amount = Convert.ToDecimal(bid[1], CultureInfo.InvariantCulture)
            };

            EthBids.Add(newBid);
        }

        return EthBids;
    }
}
