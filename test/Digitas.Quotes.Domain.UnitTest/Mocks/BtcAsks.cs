using Digitas.Quotes.Domain.Entities;
using Digitas.Quotes.Domain.ValueObjects;

namespace Digitas.Quotes.Domain.UnitTest.Mocks;

public class BtcAsks
{
    protected BtcAsks() { }

    public static List<BtcAsk> BuildBtcAskList()
    {
        return new List<BtcAsk>
        {
            new BtcAsk() { Amount = 0.72000000M, BtcAskId = 3132, Microtimestamp = 1706663869812333, UsdValue = 42851 },
            new BtcAsk() { Amount = 0.26000000M, BtcAskId = 3134, Microtimestamp = 1706663869812333, UsdValue = 42851 },
            new BtcAsk() { Amount = 0.26000000M, BtcAskId = 3133, Microtimestamp = 1706663869812333, UsdValue = 42856 },
            new BtcAsk() { Amount = 1.20000000M, BtcAskId = 3135, Microtimestamp = 1706663869812333, UsdValue = 42856 },
            new BtcAsk() { Amount = 0.76000000M, BtcAskId = 3136, Microtimestamp = 1706663869812333, UsdValue = 42857 },
            new BtcAsk() { Amount = 0.01000000M, BtcAskId = 3137, Microtimestamp = 1706663869812333, UsdValue = 42859 },
        };
    }

    public static List<Quote> BuildQuoteList()
    {
        return new List<Quote>
        {
            new Quote(0.72000000M, 42851),
            new Quote(0.26000000M, 42851),
            new Quote(0.26000000M, 42856),
            new Quote(1.20000000M, 42856),
            new Quote(0.76000000M, 42857),
            new Quote(0.01000000M, 42859)
        };
    }
}
