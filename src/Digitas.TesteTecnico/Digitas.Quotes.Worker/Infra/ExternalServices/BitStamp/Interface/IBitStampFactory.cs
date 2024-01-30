using Digitas.Quotes.Worker.Infra.ExternalServices.BitStamp.Responses;

namespace Digitas.Quotes.Worker.Infra.ExternalServices.BitStamp.Interface;

public interface IBitStampFactory
{
    BookOrderResponse FormatBitStampResponse(string receivedMessage);
}
