using MediatR;

namespace Digitas.Quotes.Worker.Application.Commands;

public record LoadQuoteBtcCommand : IRequest<bool>
{

}
