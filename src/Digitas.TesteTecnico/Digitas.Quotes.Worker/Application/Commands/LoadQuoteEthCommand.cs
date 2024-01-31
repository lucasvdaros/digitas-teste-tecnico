using MediatR;

namespace Digitas.Quotes.Worker.Application.Commands;

public record LoadQuoteEthCommand : IRequest<bool>
{

}
