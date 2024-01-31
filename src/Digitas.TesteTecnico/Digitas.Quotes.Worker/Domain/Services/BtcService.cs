using Digitas.Quotes.Worker.Domain.Entities;
using Digitas.Quotes.Worker.Domain.Interfaces;
using Digitas.Quotes.Worker.Domain.Interfaces.Repositories;
using Digitas.Quotes.Worker.Domain.Interfaces.Services;
using Digitas.Quotes.Worker.Infra.ExternalServices.BitStamp.Interface;
using Digitas.Quotes.Worker.Infra.ExternalServices.BitStamp.Responses;
using System.Globalization;
using System.Text.Json;

namespace Digitas.Quotes.Worker.Domain.Services;

public class BtcService : IBtcService
{
    private readonly IBitStampService bitStamp;
    private readonly IBtcAskRepository btcAskRepository;
    private readonly IBtcBidRepository btcBidRepository;
    private readonly IUnitOfWork uow;

    public BtcService(IBitStampService bitStamp, IBtcAskRepository btcAskRepository, IBtcBidRepository btcBidRepository, IUnitOfWork uow)
    {
        this.bitStamp = bitStamp;
        this.btcAskRepository = btcAskRepository;
        this.btcBidRepository = btcBidRepository;
        this.uow = uow;
    }

    public async Task PersistCurrentBtcBookOffers(string channel)
    {
        while (bitStamp.IsConnectionStillOpen())
        {
            var receivedMessage = await bitStamp.ReceivedMessage();

            if (!string.IsNullOrEmpty(receivedMessage))
            {
                try
                {
                    var newUpdatesData = JsonSerializer.Deserialize<BookOrderResponse>(receivedMessage);

                    if (newUpdatesData is not null && newUpdatesData.Channel.Equals(channel))
                    {
                        var newAsks = ConvertAsksToPersist(newUpdatesData.Data!);
                        var newBids = ConvertBidsToPersist(newUpdatesData.Data!);

                        Console.WriteLine($"Registering new BTC {newAsks.Count} Asks");
                        Console.WriteLine($"Registering new BTC {newBids.Count} Bids");

                        await btcAskRepository.AddRange(newAsks);
                        await btcBidRepository.AddRange(newBids);

                        uow.Commit();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(receivedMessage);
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }

    private static List<BtcAsk> ConvertAsksToPersist(BookOrderDataResponse data)
    {
        var BtcAsks = new List<BtcAsk>();

        foreach (var ask in data.Asks)
        {
            var newAsk = new BtcAsk()
            {
                Microtimestamp = Convert.ToInt64(data.Microtimestamp),
                UsdValue = Convert.ToInt32(ask[0]),
                Amount = Convert.ToDecimal(ask[1], CultureInfo.InvariantCulture)
            };

            BtcAsks.Add(newAsk);
        }

        return BtcAsks;
    }

    private static List<BtcBid> ConvertBidsToPersist(BookOrderDataResponse data)
    {
        var BtcBids = new List<BtcBid>();

        foreach (var bid in data.Bids)
        {
            var newBid = new BtcBid()
            {
                Microtimestamp = Convert.ToInt64(data.Microtimestamp),
                UsdValue = Convert.ToInt32(bid[0]),
                Amount = Convert.ToDecimal(bid[1], CultureInfo.InvariantCulture)
            };

            BtcBids.Add(newBid);
        }

        return BtcBids;
    }
}
