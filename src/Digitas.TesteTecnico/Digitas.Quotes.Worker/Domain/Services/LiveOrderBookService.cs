using Digitas.Quotes.Worker.Domain.Entities;
using Digitas.Quotes.Worker.Domain.Interfaces.Repositories;
using Digitas.Quotes.Worker.Domain.Interfaces.Services;
using Digitas.Quotes.Worker.Domain.Interfaces;
using System.Text;
using System.Text.Json;
using Digitas.Quotes.Worker.Infra.ExternalServices.BitStamp.Interface;
using Digitas.Quotes.Worker.Infra.ExternalServices.BitStamp.Responses;
using Digitas.Quotes.Worker.Infra.ExternalServices.BitStamp.Requests;
using System.Globalization;

namespace Digitas.Quotes.Worker.Domain.Services;

public class LiveOrderBookService : ILiveOrderBookService
{
    private readonly IBitStampService bitStamp;
    private readonly IBtcAskRepository btcAskRepository;
    private readonly IBtcBidRepository btcBidRepository;
    private readonly IUnitOfWork uow;

    private readonly string btcChannel = "order_book_btcusd";
    private readonly string ethChannel = "order_book_ethusd";

    public LiveOrderBookService(IUnitOfWork uow,
                                IBitStampService bitStamp,
                                IBtcAskRepository btcAskRepository,
                                IBtcBidRepository btcBidRepository)
    {
        this.bitStamp = bitStamp;
        this.btcAskRepository = btcAskRepository;
        this.btcBidRepository = btcBidRepository;
        this.uow = uow;
    }

    public async Task SearchCurrentBtcBookOffers()
    {
        bool isNotConected = !await bitStamp.IsConnectToBitStampWebSocket();

        if (isNotConected) return;

        var buffer = FormatSubscribePayload(btcChannel);

        var subscriptionResponse = await bitStamp.SubscribeChannel(buffer);

        var subscriptionStatus = new SubscriptionResponse().FormatBitStampResponse(subscriptionResponse);

        if (subscriptionStatus.IsSubscriptionSucceeded())
        {
            while (bitStamp.IsConnectionStillOpen())
            {
                var receivedMessage = await bitStamp.ReceivedMessage();

                if (!string.IsNullOrEmpty(receivedMessage))
                {
                    var newUpdatesData = JsonSerializer.Deserialize<BookOrderResponse>(receivedMessage);

                    var newAsks = ConvertAsksToPersist(newUpdatesData!.Data);
                    var newBids = ConvertBidsToPersist(newUpdatesData!.Data);

                    Console.WriteLine($"Registering new {newAsks.Count} Asks");
                    Console.WriteLine($"Registering new {newBids.Count} Bids");

                    await btcAskRepository.AddRange(newAsks);
                    await btcBidRepository.AddRange(newBids);                    

                    uow.Commit();
                }
            }
        }        
    }

    public async Task SearchCurrentEthBookOffers()
    {
        bool isNotConected = !await bitStamp.IsConnectToBitStampWebSocket();

        if (isNotConected) return;

        var buffer = FormatSubscribePayload(ethChannel);

        var subscriptionResponse = await bitStamp.SubscribeChannel(buffer);

        var subscriptionStatus = new SubscriptionResponse().FormatBitStampResponse(subscriptionResponse);

        if (subscriptionStatus.IsSubscriptionSucceeded())
        {
            while (bitStamp.IsConnectionStillOpen())
            {
                var receivedMessage = await bitStamp.ReceivedMessage();

                if (!string.IsNullOrEmpty(receivedMessage))
                {
                    var newUpdatesData = JsonSerializer.Deserialize<BookOrderResponse>(receivedMessage);


                }
            }
        }
    }

    private static byte[] FormatSubscribePayload(string btcChannel)
    {
        var data = new SubscriptionData(btcChannel);
        var subscription = new SubscriptionRequest("bts:subscribe", data);
        var subscribeMsg = JsonSerializer.Serialize(subscription);

        return Encoding.UTF8.GetBytes(subscribeMsg);
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
